using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace State
{
    //public class BankAccount
    //{
    //    public BankAccount(string accountNumber)
    //    {
    //        AccountNumber = accountNumber;
    //    }

    //    public int Balance { get; set; }

    //    public string AccountNumber { get; set; }

    //    public void Withdraw(int amount, string pin)
    //    {
    //        // here, the Balance state influences/determines the execution of the method
    //        // based on the Balance, the result will be an exception or the balance is changed
    //        if (pin != "1234")
    //            throw new UnauthorizedAccessException();

    //        if(Balance - amount < 0)
    //            throw new InvalidOperationException();

    //        Balance += amount;
    //    }

    //    public void Deposit(int amount, string pin)
    //    {
    //        if (pin != "1234")
    //            throw new UnauthorizedAccessException();

    //        Balance += amount;
    //    }
    //}

    //public abstract class BankAccount2
    //{
    //    public BankAccount2(string accountNumber)
    //    {
    //        AccountNumber = accountNumber;
    //    }

    //    public int Balance { get; set; }

    //    public string AccountNumber { get; set; }

    //    public abstract void Withdraw(int amount, string pin);

    //    public abstract void Deposit(int amount, string pin);

    //    protected void CheckPin(string pin)
    //    {
    //        if(pin != "1234")
    //            throw new UnauthorizedAccessException();
    //    }
    //}

    // Create an implementation when the variable is in state 1
    // object state will be represented by the object type
    //public class OverdrawnBackAccount : BankAccount2
    //{
    //    public OverdrawnBackAccount(string accountNumber) : base(accountNumber)
    //    {
    //    }

    //    public override void Withdraw(int amount, string pin)
    //    {
    //        throw new InvalidOperationException();
    //    }

    //    public override void Deposit(int amount, string pin)
    //    {
    //        base.CheckPin(pin);
    //        Balance += amount;
    //    }
    //}

    // Create an implementation when the variable is in state 2
    //public class NormalBackAccount : BankAccount2
    //{
    //    public NormalBackAccount(string accountNumber) : base(accountNumber)
    //    {
    //    }

    //    public override void Withdraw(int amount, string pin)
    //    {
    //        base.CheckPin(pin);
    //        Balance -= amount;
    //    }

    //    public override void Deposit(int amount, string pin)
    //    {
    //        base.CheckPin(pin);
    //        Balance += amount;
    //    }
    //}

    public class BankAccount3
    {
        // encapsulate the states which take part in the state machine to determine the state object
        private abstract class BackAccountState
        {
            protected BankAccount3 _ownerAccount;

            protected BackAccountState(BankAccount3 ownerAccount)
            {
                _ownerAccount = ownerAccount;
            }

            // TODO balance should be private set, get it from ctor
            public int Balance { get; set; }
            // no PIN here since the PIN check is a stateless operation
            public abstract void Withdraw(int amount);
            public abstract void Deposit(int amount);

            protected abstract void StateChangeCheck();
        }

        private class OverdrawnState : BackAccountState
        {
            public OverdrawnState(BankAccount3 ownerAccount) : base(ownerAccount)
            {
            }

            public override void Withdraw(int amount)
            {
                throw new InvalidOperationException();
                // state cannot change here
            }

            public override void Deposit(int amount)
            {
                Balance += amount;
                StateChangeCheck();
            }

            protected override void StateChangeCheck()
            {
                if (Balance > 0)
                    _ownerAccount.accountState = new NormalState(_ownerAccount) { Balance = Balance };
            }
        }

        private class NormalState : BackAccountState
        {
            public NormalState(BankAccount3 ownerAccount) : base(ownerAccount)
            {
            }

            public override void Withdraw(int amount)
            {
                Balance -= amount;
                StateChangeCheck();
            }

            public override void Deposit(int amount)
            {
                Balance += amount;
                // state cannot change here (add to already positive balance)
            }

            protected override void StateChangeCheck()
            {
                if(Balance <= 0)
                    _ownerAccount.accountState = new OverdrawnState(_ownerAccount) {Balance = Balance};
            }
        }

        private BackAccountState accountState;

        public BankAccount3(string accountNumber)
        {
            this.accountState = new NormalState(this);
            AccountNumber = accountNumber;
        }

        public int Balance => accountState.Balance;

        public string AccountNumber { get; set; }

        public void Withdraw(int amount, string pin)
        {
            // PIN check performed here since its stateless
            CheckPin(pin);
            accountState.Withdraw(amount);
        }

        public void Deposit(int amount, string pin)
        {
            CheckPin(pin);
            accountState.Deposit(amount);
        }

        protected void CheckPin(string pin)
        {
            if (pin != "1234")
                throw new UnauthorizedAccessException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // this does not work since the second withdraw should be not called on the normal but on the overdrawn
            //var bankAccount = new NormalBackAccount("123-ABC");
            //bankAccount.Deposit(100, "1234");
            //bankAccount.Withdraw(80, "1234");
            //bankAccount.Withdraw(70, "1234");

            var bankAccount = new BankAccount3("ABC-123");
            bankAccount.Deposit(1000, "1234");
            bankAccount.Withdraw(500, "1234");
            bankAccount.Withdraw(5000, "1234");
            bankAccount.Deposit(6000, "1234");
        }
    }
}
