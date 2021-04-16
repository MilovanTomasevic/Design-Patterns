package memento

import (
  "fmt"
)

type Memento struct {
  Balance int
}

type BankAccount struct {
  balance int
}

func (b *BankAccount) Deposit(amount int) *Memento {
  b.balance += amount
  return &Memento{b.balance}
}

func (b *BankAccount) Restore(m *Memento) {
  b.balance = m.Balance
}

func main() {
  ba := BankAccount{100}
  m1 := ba.Deposit(50)
  m2 := ba.Deposit(25)
  fmt.Println(ba)

  ba.Restore(m1)
  fmt.Println(ba) // 150

  ba.Restore(m2)
  fmt.Println(ba)
}