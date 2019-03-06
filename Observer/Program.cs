using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Observer
{
    // event-es
    public class BankAccount
    {
        public BankAccount(string accountNo)
        {
            AccountNo = accountNo;
            _balance = 150;
        }

        public string AccountNo { get; private set; }
        private int _balance;

        public int Balance
        {
            get { return _balance; }
            set
            {
                int original = _balance;
                _balance = value;
                OnBalanceChanged(_balance, original);
            }
        }

        public event EventHandler<BalanceChangedEventArgs> BalanceChanged;

        protected virtual void OnBalanceChanged(int newBalance, int oldBalance)
        {
            var args = new BalanceChangedEventArgs(DateTime.Now, newBalance, oldBalance);

            BalanceChanged?.Invoke(this, args);

            //or before c#6
            // if BalanceChanged!= null { var temp = BalanceChanged; if temp != null temp(this, args) }
        }

        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            if(Balance - amount < 0)
                throw new InvalidOperationException();

            Balance -= amount;
        }
    }

    public class BalanceChangedEventArgs : EventArgs
    {
        public BalanceChangedEventArgs(DateTime changeDate, int originalBalance, int newBalance)
        {
            ChangeDate = changeDate;
            OriginalBalance = originalBalance;
            NewBalance = newBalance;
        }

        public DateTime ChangeDate { get; private set; }
        public int OriginalBalance { get; private set; }
        public int NewBalance { get; private set; }
    }

    public class ObservableBankAccount
    {
        public ObservableBankAccount(string accountNo)
        {
            AccountNo = accountNo;
            _balance = 150;
        }

        public string AccountNo { get; private set; }
        private int _balance;

        public int Balance
        {
            get { return _balance; }
            set
            {
                int original = _balance;
                _balance = value;
                Notify(_balance, original);
            }
        }

        private readonly List<IBalanceNotifier> observers = new List<IBalanceNotifier>();

        public void Attach(IBalanceNotifier o) => observers.Add(o);

        public void Detach(IBalanceNotifier o) => observers.Remove(o);

        protected virtual void Notify(int newBalance, int oldBalance)
        {
            var args = new BalanceChangedEventArgs(DateTime.Now, newBalance, oldBalance);

            // sorrendet + hibakezelest en kontrollalom
            foreach (var observer in observers)
            {
                try
                {
                    observer.Update(this, args);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Observer {0} threw exception {e}");
                }
            }
        }

        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            if (Balance - amount < 0)
                throw new InvalidOperationException();

            Balance -= amount;
        }
    }

    public interface IBalanceNotifier
    {
        // event-es 
        //void Update(object sender, BalanceChangedEventArgs args);
        void Update(ObservableBankAccount sender, BalanceChangedEventArgs args);
    }

    public class SmsBalanceNotifier : IBalanceNotifier
    {
        // event-es 
        //public void Update(object sender, BalanceChangedEventArgs args)
        public void Update(ObservableBankAccount sender, BalanceChangedEventArgs args)
        {
            Console.WriteLine("Ping");
            // event-es 
            //var ba = (BankAccount) sender;
            Console.WriteLine($"Account {sender.AccountNo}: On {args.ChangeDate} from {args.OriginalBalance} to {args.NewBalance}");
        }
    }

    public class EmailBalanceNotifier : IBalanceNotifier
    {
        // event-es 
        //public void Update(object sender, BalanceChangedEventArgs args)
        public void Update(ObservableBankAccount sender, BalanceChangedEventArgs args)
        {
            Console.WriteLine("Csirip");
            // event-es 
            //var ba = (BankAccount)sender;
            Console.WriteLine($"<h1> Account {sender.AccountNo}: On {args.ChangeDate} from {args.OriginalBalance} to {args.NewBalance} </h1>");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // event-es 
            //var bankAccount = new BankAccount("ABC-123");
            var bankAccount = new ObservableBankAccount("ABC-123");

            var smsBalanceNotifier = new SmsBalanceNotifier();
            var emailBalanceNotifier = new EmailBalanceNotifier();
            // event-es 
            //bankAccount.BalanceChanged += smsBalanceNotifier.Update;
            //bankAccount.BalanceChanged += emailBalanceNotifier.Update;

            bankAccount.Attach(smsBalanceNotifier);
            bankAccount.Attach(emailBalanceNotifier);

            bankAccount.Withdraw(50);

            Console.ReadKey();
        }
    }
}
