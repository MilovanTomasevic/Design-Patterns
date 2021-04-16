import Foundation

class Memento
{
  let balance: Int // immutable
  init(_ balance: Int)
  {
    self.balance = balance
  }
}

class BankAccount : CustomStringConvertible
{
  private var balance: Int

  init(_ balance: Int)
  {
    self.balance = balance
  }

  func deposit(_ amount: Int) -> Memento
  {
    balance += amount
    return Memento(balance)
  }

  func restore(_ m: Memento)
  {
    balance = m.balance
  }

  public var description: String
  {
    return "Balance = \(balance)"
  }
}

func main()
{
  var ba = BankAccount(100)
  let m1 = ba.deposit(50) // 150
  let m2 = ba.deposit(25) // 175
  print(ba)

  // restore to m1
  ba.restore(m1)
  print(ba)

  // restore to m1
  ba.restore(m2)
  print(ba)

  // there is no memento for the initial state
}

main()