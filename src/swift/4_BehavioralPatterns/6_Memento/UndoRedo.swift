import Foundation

class Memento
{
  let balance: Int
  init(_ balance: Int)
  {
    self.balance = balance
  }
}

class BankAccount : CustomStringConvertible
{
  private var balance: Int
  private var changes = [Memento]()
  private var current = 0

  init(_ balance: Int)
  {
    self.balance = balance
    changes.append(Memento(balance))
  }

  func deposit(_ amount: Int) -> Memento
  {
    balance += amount
    let m = Memento(balance)
    changes.append(m)
    current += 1
    return m
  }

  func restore(_ m: Memento?)
  {
    if let mm = m
    {
      balance = mm.balance
      changes.append(mm)
      current = changes.count - 1
    }
  }

  func undo() -> Memento?
  {
    if current > 0
    {
      current -= 1
      let m = changes[current]
      balance = m.balance
      return m
    }
    return nil
  }

  func redo() -> Memento?
  {
    if (current+1) < changes.count
    {
      current += 1
      let m = changes[current]
      balance = m.balance
      return m
    }
    return nil
  }

  public var description : String
  {
    return "Balance = \(balance)"
  }
}

func main()
{
  var ba = BankAccount(100)
  ba.deposit(50)
  ba.deposit(25)
  print(ba)

  ba.undo()
  print("Undo 1: \(ba)")
  ba.undo()
  print("Undo 2: \(ba)")
  ba.redo()
  print("Redo 2: \(ba)")
}

main()