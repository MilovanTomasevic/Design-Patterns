import Foundation

protocol Log
{
  func info(_ msg: String)
  func warn(_ msg: String)
}

class ConsoleLog : Log
{
  func info(_ msg: String)
  {
    print(msg)
  }

  func warn(_ msg: String)
  {
    print("WARNING: \(msg)")
  }
}

class BankAccount
{
  var log: Log
  var balance = 0

  init(_ log: Log)
  {
    self.log = log
  }

  func deposit(_ amount: Int)
  {
    balance += amount
    // check for null everywhere?
    log.info("Deposited \(amount), balance is now \(balance)")
  }

  func withdraw(_ amount: Int)
  {
    if (balance >= amount)
    {
      balance -= amount
      log.info("Withdrew \(amount), we have \(balance) left")
    }
    else
    {
      log.warn("Could not withdraw \(amount) because balance is only \(balance)")
    }
  }
}

class NullLog : Log
{
  func info(_ msg: String) {}
  func warn(_ msg: String) {}
}

func main()
{
  //let log = ConsoleLog()
  //let log = nil
  let log = NullLog()
  let ba = BankAccount(log)
  ba.deposit(100)
  ba.withdraw(200)
}

main()