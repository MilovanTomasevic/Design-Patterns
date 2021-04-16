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

class Game
{
  var ratEnters = Event<AnyObject>()
  var ratDies   = Event<AnyObject>()
  var notifyRat = Event<(AnyObject,Rat)>()

  func fireRatEnters(_ sender: AnyObject)
  {
    ratEnters.raise(sender)
  }

  func fireRatDies(_ sender: AnyObject)
  {
    ratDies.raise(sender)
  }

  func fireNotifyRat(_ sender: AnyObject, _ whichRat: Rat)
  {
    notifyRat.raise(sender, whichRat)
  }
}

class Rat
{
  private let game: Game
  var attack = 1

  init(_ game: Game)
  {
    self.game = game

    game.ratEnters.addHandler(
      target: self,
      handler: {
        (_) -> ((AnyObject)) -> () in
        return {
          if $0 !== self
          {
            self.attack += 1
            game.fireNotifyRat(self, $0 as! Rat)
          }
        }
      }
    )

    game.ratDies.addHandler(
      target: self,
      handler: {
        (_) -> ((AnyObject)) -> () in
        return {
          if $0 !== self
          {
            self.attack -= 1
          }
        }
      }
    )

    game.notifyRat.addHandler(
      target: self,
      handler: {
        (_) -> ((AnyObject, Rat)) -> () in
        return {
          if $1 === self
          {
            self.attack += 1
          }
        }
      }
    )

    game.fireRatEnters(self)
  }

  func kill() {
    game.fireRatDies(self)
  }
}

class UMBaseTestCase : XCTestCase {}

class Evaluate: UMBaseTestCase
{
  func singleRatTest()
  {
    let game = Game()
    let rat = Rat(game)
    XCTAssertEqual(1, rat.attack)
  }

  func twoRatTest()
  {
    let game = Game()
    let rat = Rat(game)
    let rat2 = Rat(game)
    XCTAssertEqual(2, rat.attack)
    XCTAssertEqual(2, rat2.attack)
  }

  func threeRatsOneDies()
  {
    let game = Game()

    let rat = Rat(game)
    XCTAssertEqual(1, rat.attack)

    let rat2 = Rat(game)
    XCTAssertEqual(2, rat.attack)
    XCTAssertEqual(2, rat2.attack)

    do {
      var rat3 = Rat(game)

      XCTAssertEqual(3, rat.attack)
      XCTAssertEqual(3, rat2.attack)
      XCTAssertEqual(3, rat3.attack)

      rat3.kill()
    }

    let msg = "When the 3rd rat dies, the remaining rats' attack value should be =2"
    XCTAssertEqual(2, rat.attack, msg)
    XCTAssertEqual(2, rat2.attack, msg)
  }
}

extension Evaluate
{
  static var allTests : [(String, (Evaluate) -> () throws -> Void)]
  {
    return [
      ("singleRatTest", singleRatTest),
      ("twoRatTest", twoRatTest),
      ("threeRatsOneDies", threeRatsOneDies)
    ]
  }
}

func main()
{
  XCTMain([testCase(Evaluate.allTests)])
}

main()