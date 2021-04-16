package templatemethod

import "fmt"

type Game interface {
  Start()
  HaveWinner() bool
  TakeTurn()
  WinningPlayer() int
}

func PlayGame(g Game) {
  g.Start()
  for ;!g.HaveWinner(); {
    g.TakeTurn()
  }
  fmt.Printf("Player %d wins.\n", g.WinningPlayer())
}

type chess struct {
  turn, maxTurns, currentPlayer int
}

func NewGameOfChess() Game {
  return &chess{ 1, 10, 0 }
}

func (c *chess) Start() {
  fmt.Println("Starting a game of chess.")
}

func (c *chess) HaveWinner() bool {
  return c.turn == c.maxTurns
}

func (c *chess) TakeTurn() {
  c.turn++
  fmt.Printf("Turn %d taken by player %d\n",
    c.turn, c.currentPlayer)
  c.currentPlayer = (c.currentPlayer + 1) % 2
}

func (c *chess) WinningPlayer() int {
  return c.currentPlayer
}

func main() {
  chess := NewGameOfChess()
  PlayGame(chess)
}