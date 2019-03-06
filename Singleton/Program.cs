using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class Singleton
    {
        // private ctor blocks derived classes since the derived class's ctor needs to call a base ctor
        // or use sealed
        private Singleton()
        {
        }

        // ez .NET-ben mind overkill, ld. lent
        //private static readonly object lockObject = new object();

        //private static volatile Singleton _instance;
        //public static Singleton Instance
        //{
        //    get
        //    {
        //        // double-checked locking: if-lock-if
        //        // still not optimal since a thread can enter to the first if, the other completes, the first continues
        //        if (_instance == null)
        //        {
        //            lock (lockObject)
        //            {
        //                // test and set
        //                // problem: not atomic
        //                if (_instance == null)
        //                {
        //                    _instance = new Singleton();
        //                }
        //            } 
        //        }

        //        return _instance;
        //    }
        //}


        // .NET specific solution v1
        // since static instance initializations are guaranteed by CLR to be performed only once 
        // time of instantiation is not guaranteed, only that it will be performed when the class is first referenced
        //private static readonly Singleton _instance = new Singleton();

        //public static Singleton Instance => _instance;

        // .NET specific solution v2
        // access only when necessary, and the called time is known
        // without ctor func<T>, the default ctor is called, but here T has none 
        private static readonly Lazy<Singleton> instance = new Lazy<Singleton>(() => new Singleton(), true);

        public static Singleton Instance => instance.Value;

        // vagy egy sorban
        //public static Lazy<Singleton> Instance { get; } = new Lazy<Singleton>(() => new Singleton(), true);

        public void DoSomething()
        {
            Console.WriteLine("Something");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var singleton = Singleton.Instance;

            singleton.DoSomething();

            Console.ReadKey();
        }
    }
}
