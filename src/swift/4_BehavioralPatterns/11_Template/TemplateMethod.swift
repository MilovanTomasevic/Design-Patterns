import Foundation
import XCTest

// no abstract classes, so we fail in abstract methods
class Game
{
  func run() // this is the template method
  {
    start()
    while !haveWinner
    {
      takeTurn()
    }
    print("Player \(winningPlayer) wins!")
  }

  internal func start()
  {
    precondition(false, "this method needs to be overridden")
  }

  internal var haveWinner: Bool
  {
    get {
      precondition(false, "this method needs to be overridden")
    }
  }

  internal func takeTurn()
  {
    precondition(false, "this method needs to be overridden")
  }

  internal var winningPlayer: Int
  {
    get {
      precondition(false, "this method needs to be overridden")
    }
  }

  internal var currentPlayer = 0
  internal let numberOfPlayers: Int

  init(_ numberOfPlayers: Int)
  {
    self.numberOfPlayers = numberOfPlayers
  }
}

// simulate a game of chess
class Chess : Game
{
  private let maxTurns = 10
  private var turn = 1

  init() {
    super.init(2)
  }

  override func start()
  {
    print("Starting a game of chess with \(numberOfPlayers) players.")
  }

  override var haveWinner: Bool
  {
    return turn == maxTurns
  }

  override func takeTurn()
  {
    print("Turn \(turn) taken by player \(currentPlayer).")
    currentPlayer = (currentPlayer + 1) % numberOfPlayers
    turn += 1
  }

  override var winningPlayer: Int
  {
    return currentPlayer
  }
}

func main()
{
  let chess = Chess()
  chess.run()
}

main()