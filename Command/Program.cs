using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Command
{
	// this is the Receiver in the pattern -- the component whose methods will be called
	public class TargetClass
	{
		public static void DoSomething()
		{
			Console.WriteLine("Doing something static");
		}

		public static void DoSomethingWithParameters(int a, string b)
		{
			Console.WriteLine("Doing something else");
			Console.WriteLine(a);
			Console.WriteLine(b);
		}

		public void DoSomethingOO()
		{
			Console.WriteLine("OO");
		}
	}

	// Command
	public interface ICommand
	{
		void Execute();
	}

	// ConcreteCommand
	public class DoSomethingCommand : ICommand
	{
		public void Execute()
		{
			TargetClass.DoSomething();
		}
	}

	public class DoSomethingWithParametersCommand : ICommand
	{
		private readonly int parameter1;
		private readonly string parameter2;

		public DoSomethingWithParametersCommand(int parameter1, string parameter2)
		{
			this.parameter1 = parameter1;
			this.parameter2 = parameter2;
		}

		public void Execute()
		{
			TargetClass.DoSomethingWithParameters(parameter1, parameter2);
		}
	}

	public class DoSomethingOOCommand : ICommand
	{
		private TargetClass target;

		public DoSomethingOOCommand(TargetClass target)
		{
			this.target = target;
		}

		public void Execute()
		{
			target.DoSomethingOO();
		}
	}

    // Invoker in the pattern
	public class Invoker
	{
	    public void Invoke(ICommand c)
	    {
	        int i = 0;
            while (i < 3)
            {
                try
                {
                    c.Execute();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    i++;
                    Thread.Sleep(500);
                } 
            }
	    }
	}

    public class Invoker2
    {
        public void Invoke(ICommand c)
        {
            Console.WriteLine($"{DateTime.Now}: Invoking {c}");
            c.Execute();
        }
    }

    public class Invoker3
    {
        public void Invoke(ICommand c)
        {
            var sw = Stopwatch.StartNew();
            c.Execute();
            sw.Stop();
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");
        }
    }

	class Program
	{
		// Client int the pattern
		static void Main(string[] args)
		{
            Invoker invoker = new Invoker();

			//TargetClass.DoSomething();
			ICommand c1 = new DoSomethingCommand();
            invoker.Invoke(c1);
			//TargetClass.DoSomethingWithParameters(7, "Lo");
			ICommand c2 = new DoSomethingWithParametersCommand(7, "Lo");
            invoker.Invoke(c2);
			var tc = new  TargetClass();
			//tc.DoSomethingOO();
			ICommand c3 = new DoSomethingOOCommand(tc);
            invoker.Invoke(c3);

			//List<ICommand> commands = new List<ICommand>() { c1, c2, c3 };
		}
	}
}
