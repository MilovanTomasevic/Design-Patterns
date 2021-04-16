package memento

import "fmt"

type Memento struct {
  Balance int
}

type BankAccount struct {
  balance int
  changes []*Memento
  current int
}

func (b *BankAccount) String() string {
  return fmt.Sprint("Balance = $", b.balance,
    ", current = ", b.current)
}

func NewBankAccount(balance int) *BankAccount {
  b := &BankAccount{balance: balance}
  b.changes = append(b.changes, &Memento{balance})
  return b
}

func (b *BankAccount) Deposit(amount int) *Memento {
  b.balance += amount
  m := Memento{b.balance}
  b.changes = append(b.changes, &m)
  b.current++
  fmt.Println("Deposited", amount,
    ", balance is now", b.balance)
  return &m
}

func (b *BankAccount) Restore(m *Memento) {
  if m != nil {
    b.balance -= m.Balance
    b.changes = append(b.changes, m)
    b.current = len(b.changes) - 1
  }
}

func (b *BankAccount) Undo() *Memento {
  if b.current > 0 {
    b.current--
    m := b.changes[b.current]
    b.balance = m.Balance
    return m
  }
  return nil // nothing to undo
}

func (b *BankAccount) Redo() *Memento {
  if b.current + 1 < len(b.changes) {
    b.current++
    m := b.changes[b.current]
    b.balance = m.Balance
    return m
  }
  return nil
}

func main() {
  ba := NewBankAccount(100)
  ba.Deposit(50)
  ba.Deposit(25)
  fmt.Println(ba)

  ba.Undo()
  fmt.Println("Undo 1:", ba)
  ba.Undo()
  fmt.Println("Undo 2:", ba)
  ba.Redo()
  fmt.Println("Redo:", ba)
}