import Foundation
import XCTest

public protocol Disposable
{
  func dispose()
}

protocol Invocable : class
{
  func invoke(_ data: Any)
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
    (target: U, handler: @escaping (U) -> EventHandler) -> Disposable
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

class Participant
{
  private let mediator: Mediator
  var value = 0

  init(_ mediator: Mediator)
  {
    self.mediator = mediator
    mediator.alert.addHandler(
      target: self,
      handler: {
        (_) -> ((AnyObject, Int)) -> () in
        return self.alert
      }
    )
  }

  func alert(_ data: (AnyObject, Int))
  {
    if (data.0 !== self)
    {
      value += data.1
    }
  }

  func say(_ n: Int)
  {
    mediator.broadcast(self, n)
  }
}

class Mediator
{
  let alert = Event<(AnyObject, Int)>()

  func broadcast(_ sender: AnyObject, _ n: Int)
  {
    alert.raise(sender, n)
  }
}

class UMBaseTestCase : XCTestCase {}

class Evaluate: UMBaseTestCase
{
  func simpleTest()
  {
    let mediator = Mediator()
    let p1 = Participant(mediator)
    let p2 = Participant(mediator)

    XCTAssertEqual(0, p1.value)
    XCTAssertEqual(0, p2.value)

    p1.say(2)

    XCTAssertEqual(0, p1.value)
    XCTAssertEqual(2, p2.value)

    p2.say(4)

    XCTAssertEqual(4, p1.value)
    XCTAssertEqual(2, p2.value)
  }
}

extension Evaluate
{
  static var allTests : [(String, (Evaluate) -> () throws -> Void)]
  {
    return [
      ("simpleTest", simpleTest)
    ]
  }
}

func main()
{
  XCTMain([testCase(Evaluate.allTests)])
}

main()