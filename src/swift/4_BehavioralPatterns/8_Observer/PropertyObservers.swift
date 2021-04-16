import Foundation
import XCTest

protocol Invocable : class
{
  func invoke(_ data: Any)
}

public protocol Disposable
{
  func dispose()
}

public class Event<T>
{
  public typealias EventHandler = (T) -> ()

  var eventHandlers = [Invocable]()

  public func raise(_ data: T)
  {
    for handler in self.eventHandlers
    {
      handler.invoke(data)
    }
  }

  public func addHandler<U: AnyObject>
    (_ target: U, _ handler: @escaping (U) -> EventHandler) -> Disposable
  {
    let subscription = Subscription(target: target, handler: handler, event: self)
    eventHandlers.append(subscription)
    return subscription
  }
}

class Subscription<T: AnyObject, U> : Invocable, Disposable
{
  weak var target: T?
  let handler: (T) -> (U) -> ()
  let event: Event<U>

  init(target: T?, handler: @escaping (T) -> (U) -> (), event: Event<U>)
  {
    self.target = target
    self.handler = handler
    self.event = event
  }

  func invoke(_ data: Any) {
    if let t = target {
      handler(t)(data as! U)
    }
  }

  func dispose()
  {
    event.eventHandlers = event.eventHandlers.filter { $0 as AnyObject? !== self }
  }
}

class Person
{
  private var oldCanVote = false
  var age: Int = 0
  {
    willSet(newValue)
    {
      print("About to set age to \(newValue)")
      oldCanVote = canVote // somewhat silly
    }
    didSet // cannot have a set specifier!
    {
      print("We just changed age from \(oldValue) to \(age)")

      if age != oldValue // really did change
      {
        propertyChanged.raise(("age", age))
      }

      // did we affect canVote?
      if canVote != oldCanVote
      {
        propertyChanged.raise(("canVote", canVote))
      }
    }
  }

  var canVote: Bool // dependent property
  {
    get {
      return age >= 16
      // now do we get notifications here?
    }
  }

  // C# style property changed notification
  let propertyChanged = Event<(String, Any)>()
}

// pass a bool by reference without the 'inout' bother
final class RefBool
{
  var value: Bool
  init(_ value: Bool)
  {
    self.value = value
  }
}

class Person2
{
  private var _age: Int = 0
  var age: Int
  {
    get {
      return _age
    }

    set(value) {
      if _age == value { return } // no change happened

      // cache all dependent properties
      let oldCanVote = canVote

      // does anyone want to prevent this property being set?
      var cancelSet = RefBool(false)
      propertyChanging.raise(("age", value, cancelSet))
      if (cancelSet.value)
      {
        return // someone canceled
      }

      // assign and notify
      _age = value
      propertyChanged.raise(("age", _age))

      // check all dependants that changed
      if oldCanVote != canVote
      {
        propertyChanged.raise(("canVote", canVote))
      }
    }
  }

  var canVote: Bool
  {
    return _age >= 16
  }

  let propertyChanged = Event<(String, Any)>()

  // feel free to also implement
  let propertyChanging = Event<(String, Any, RefBool)>()
  // for handling value that's about to change
}

class Demo
{
  init()
  {
    let p = Person()
    p.propertyChanged.addHandler(self, Demo.propChanged)
    p.age = 20
    p.age = 22 // voting status shouldn't change

    print("===")

    let p2 = Person2()
    p2.propertyChanged.addHandler(self, Demo.propChanged)
    p2.propertyChanging.addHandler(self, Demo.propChanging)
    p2.age = 20
    p2.age = 22

    p2.age = 12 // disallowed
  }

  func propChanging(args: (String, Any, RefBool))
  {
    if args.0 == "age" && (args.1 as! Int) < 14
    {
      print("Cannot allow to set the age lower than 14")
      args.2.value = true // cancel this
    }
  }

  func propChanged(args: (String, Any))
  {
    if args.0 == "age"
    {
      print("Person's age has been changed to \(args.1)")
    }
    else if args.0 == "canVote"
    {
      print("Voting status has been changed to \(args.1)")
    }
  }
}

let _ = Demo()