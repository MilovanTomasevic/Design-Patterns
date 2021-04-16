import Foundation

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
    for handler in eventHandlers
    {
      handler.invoke(data)
    }
  }

  public func addHandler<U: AnyObject>
    (target: U, handler: @escaping (U) -> EventHandler) -> Disposable
  {
    let subscription = Subscription(
      target: target, handler: handler, event: self)
    eventHandlers.append(subscription)
    return subscription
  }
}

class Subscription<T: AnyObject, U> : Invocable, Disposable
{
  weak var target: T? // note: weak reference!
  let handler: (T) -> (U) -> ()
  let event: Event<U>

  init(target: T?, 
       handler: @escaping (T) -> (U) -> (), 
       event: Event<U>)
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
  let fallsIll = Event<String>()
  init() {}
  func catchACold()
  {

  }
}

class Demo
{
  init() {
    let p = Person()
    let sub = p.fallsIll.addHandler(target: self, handler:Demo.callDoctor)

    // emulate person being ill
    p.fallsIll.raise("123 London Road")

    // no longer interested in the subscription
    sub.dispose()
  }

  func callDoctor(address: String)
  {
    print("We need a doctor at \(address)")
  }
}

func main()
{
  let _ = Demo()
}

main()