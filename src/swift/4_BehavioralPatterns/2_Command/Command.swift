import Foundation

class BankAccount : CustomStringConvertible
{
  private var balance: Int = 0
  private let overdraftLimit = -500

  func deposit(_ amount: Int)
  {
    balance += amount
    print("Deposited \(amount), balance is now \(balance)")
  }

  func withdraw(_ amount: Int) -> Bool
  {
    if (balance - amount >= overdraftLimit)
    {
      balance -= amount
      print("Withdrew \(amount), balance is now \(balance)")
      return true
    }
    return false
  }

  var description: String
  {
    return "Balance: \(balance)"
  }
}

protocol Command
{
  func call()
  func undo()
}

class BankAccountCommand : Command
{
  private var account: BankAccount

  enum Action
  {
    case deposit
    case withdraw
  }

  private var action: Action
  private var amount: Int
  private var succeeded: Bool = false

  init(_ account: BankAccount, _ action: Action, _ amount: Int)
  {
    self.account = account
    self.action = action
    self.amount = amount
  }

  func call()
  {
    switch action
    {
      case .deposit:
        account.deposit(amount)
        succeeded = true
      case .withdraw:
        succeeded = account.withdraw(amount)
    }
  }

  func undo()
  {
    if !succeeded { return }

    // a rather bizarre way of doing things
    // not production code! :)
    switch action
    {
      case .deposit:
        account.withdraw(amount)
      case .withdraw:
        account.deposit(amount)
    }
  }
}

func main()
{
  let ba = BankAccount()
  let commands = [
    BankAccountCommand(ba, .deposit, 100),
    BankAccountCommand(ba, .withdraw, 25) // try 1000
  ]

  print("Initial account: \(ba)")

  // apply each of the commands
  commands.forEach{ $0.call() }

  print("After commands applied: \(ba)")

  // undo commands in reverse order
  commands.reversed().forEach{ $0.undo() }

  print("After undo: \(ba)")
}

main()