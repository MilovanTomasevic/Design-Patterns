class BankAccount
{
  constructor(balance=0)
  {
    this.balance = balance;
  }

  deposit(amount)
  {
    this.balance += amount;
    console.log(
      `Deposited ${amount}, balance is now ${this.balance}`
    );
  }

  withdraw(amount)
  {
    if (this.balance - amount >= BankAccount.overdraftLimit)
    {
      this.balance -= amount;
      console.log(
        `Withdrew ${amount}, balance is now ${this.balance}`
      );
      return true;
    }
    return false;
  }

  toString()
  {
    return `Balance: ${this.balance}`;
  }
}
BankAccount.overdraftLimit = -500;

let Action = Object.freeze({
  'deposit': 1,
  'withdraw': 2
});

class BankAccountCommand
{
  constructor(account, action, amount)
  {
    this.account = account;
    this.action = action;
    this.amount = amount;
    this.succeeded = false;
  }

  call()
  {
    switch (this.action)
    {
      case Action.deposit:
        this.account.deposit(this.amount);
        this.succeeded = true;
        break;
      case Action.withdraw:
        this.succeeded = this.account.withdraw(this.amount);
        break;
    }
  }

  undo()
  {
    if (!this.succeeded) return;
    switch (this.action)
    {
      case Action.deposit:
        this.account.withdraw(this.amount);
        break;
      case Action.withdraw:
        this.account.deposit(this.amount);
        break;
    }
  }
}

let ba = new BankAccount(100);

let cmd = new BankAccountCommand(ba, Action.deposit, 50);
cmd.call();
console.log(ba.toString());

console.log('Performing undo:');
cmd.undo();
console.log(ba.toString());