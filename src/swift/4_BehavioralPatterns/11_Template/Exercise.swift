import Foundation
import XCTest

class Creature
{
  public var attack, health: Int

  init(_ attack: Int, _ health: Int)
  {
    self.attack = attack
    self.health = health
  }
}

class CardGame
{
  var creatures: [Creature]

  init(_ creatures: [Creature])
  {
    self.creatures = creatures
  }

  // returns the index of the reature that won the fight
  // returns -1 if there is no clear winner (both alive or both dead)
  func combat(_ creature1: Int, _ creature2: Int) -> Int
  {
    let first = creatures[creature1]
    let second = creatures[creature2]
    hit(first, second)
    hit(second, first)
    let firstAlive = (first.health > 0)
    let secondAlive = (second.health > 0)
    if firstAlive == secondAlive
    {
      return -1
    }
    return firstAlive ? creature1 : creature2
  }

  internal func hit(_ attacker: Creature, _ other: Creature)
  {
    precondition(false, "this method needs to be overridden")
  }
}

class TemporaryCardDamageGame : CardGame
{
  override func hit(_ attacker: Creature, _ other: Creature)
  {
    let oldHealth = other.health
    other.health -= attacker.attack
    if other.health > 0
    {
      other.health = oldHealth
    }
  }
}

class PermanentCardDamage : CardGame
{
  override func hit(_ attacker: Creature, _ other: Creature)
  {
    other.health -= attacker.attack
  }
}

class UMBaseTestCase : XCTestCase {}

class Evaluate: UMBaseTestCase
{
  func impasseTest()
  {
    let c1 = Creature(1, 2)
    let c2 = Creature(1, 2)
    let game = TemporaryCardDamageGame([c1, c2])
    XCTAssertEqual(-1, game.combat(0, 1))
    XCTAssertEqual(-1, game.combat(0, 1))
  }

  func temporaryMurderTest()
  {
    let c1 = Creature(1, 1)
    let c2 = Creature(2, 2)
    let game = TemporaryCardDamageGame([c1, c2])
    XCTAssertEqual(1, game.combat(0, 1))
  }

  func doubleMurderTest()
  {
    let c1 = Creature(2, 2)
    let c2 = Creature(2, 2)
    let game = TemporaryCardDamageGame([c1, c2])
    XCTAssertEqual(-1, game.combat(0, 1))
  }

  func permanentDamageDeathTest()
  {
    let c1 = Creature(1, 2)
    let c2 = Creature(1, 3)
    let game = PermanentCardDamage([c1, c2])
    XCTAssertEqual(-1, game.combat(0, 1))
    XCTAssertEqual(1, game.combat(0, 1))
  }
}

extension Evaluate
{
  static var allTests : [(String, (Evaluate) -> () throws -> Void)]
  {
    return [
      ("impasseTest", impasseTest),
      ("temporaryMurderTest", temporaryMurderTest),
      ("doubleMurderTest", doubleMurderTest),
      ("permanentDamageDeathTest", permanentDamageDeathTest)
    ]
  }
}

func main()
{
  XCTMain([testCase(Evaluate.allTests)])
}

main()