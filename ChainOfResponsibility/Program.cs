using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    public abstract class BankEmployee
    {
        protected BankEmployee nextHandler;

        //public abstract bool ApproveLoan(int amount);

        // + Template pattern
        public abstract bool CanHandle(int amount);
        public abstract bool Handle(int amount);

        public bool ApproveLoanTemplate(int amount)
        {
            if (CanHandle(amount))
                return Handle(amount);

            return nextHandler.ApproveLoanTemplate(amount);
        }
    }

    public class Teller : BankEmployee
    {
        public Teller()
        {
            nextHandler = new Manager();
        }

        // problem with this: error prone - a handler should call the next 
        //Can lead to Code duplication 
						
        //-> solution: combine with Template pattern
        //public override bool ApproveLoan(int amount)
        //{
        //    // the < 5000 logic is here
        //    if(amount < 5000)
        //        return amount % 2 == 0 && DateTime.Now.Day == 1;

        //    return nextHandler.ApproveLoan(amount);
        //}

        public override bool CanHandle(int amount)
        {
            return amount < 5000;
        }

        public override bool Handle(int amount)
        {
            return amount % 2 == 0 && DateTime.Now.Day == 1;
        }
    }

    public class Manager : BankEmployee
    {
        public Manager()
        {
            nextHandler = new Director();
        }

        //public override bool ApproveLoan(int amount)
        //{
        //    if (amount < 10000)
        //        return (amount - 18) % 7 == 0;

        //    return nextHandler.ApproveLoan(amount);
        //}

        public override bool CanHandle(int amount)
        {
            return amount < 10000;
        }

        public override bool Handle(int amount)
        {
            return (amount - 18) % 7 == 0;
        }
    }

    public class Director : BankEmployee
    {
        //public override bool ApproveLoan(int amount)
        //{
        //    // the chain ends here
        //    int x = new Random().Next(256);
        //    return x == 'M';
        //}

        public override bool CanHandle(int amount)
        {
            return true;
        }

        public override bool Handle(int amount)
        {
            int x = new Random().Next(256);
            return x == 'M';
        }
    }

    public class Bank
    {
        private readonly Teller teller = new Teller();

        public bool GetLoan(int amount)
        {
            // wrong -- working but multiple issues 
            // > hard to test
            // > new rule or actor -> an other else if 
            //      have to rerun every test
            // > the creator of bank should not be concerned about the loan structure of the org. 
            //if(amount < 5000)
            //    return ApproveByTeller(amount)
            //else if (amount < 10000)
            //    return ApproveByManager(amount)
            //else
            //    ApproveByDirector(amount)

            //ApproveByTeller(int amount) { ... }

            return teller.ApproveLoanTemplate(amount);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var bank = new Bank();
            Console.WriteLine(bank.GetLoan(15000));
        }
    }
}
