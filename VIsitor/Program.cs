using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Visitor
{
    public class Person
    {
        
    }

    public class Bartender : Person
    {
        
    }

    public class Auto
    {
        public virtual void Drive(Person p)
        {
            Console.WriteLine("Autodrive + p");
        }

        public virtual void Drive(Bartender b)
        {
            Console.WriteLine("Autodrive + b");
        }
    }

    public class Teherauto : Auto
    {
        public override void Drive(Person p)
        {
            Console.WriteLine("teherauto drive + p");
        }

        public virtual void Drive(Bartender b)
        {
            Console.WriteLine("teherauto drive + b");
        }
    }

    class Program
    {
        static void M1(Person p)
        {
        }

        static void M1(Bartender b)
        {
        }

            
        static void Main(string[] args)
        {
            Person a = new Person();
            Bartender b = new Bartender();
            Person c = new Bartender();

            M1(a);
            M1(b);
            M1(c); // Person-os hivodik mivel forditasi forditasi/statikus ideju tipus szamit
                   // futasi ideju tipus szamit pl virtual methodoknál, ld lent

            Auto x = new Auto();
            Teherauto y = new Teherauto();
            Auto z = new Teherauto();
            
            // 1. forditasi idoben overload kivalasztasa parameter alapjan (Overload resolution)
            //      ehhez forditasi ideju tipust hasznal
            // 2. Futasi idoben eldol hogy az oroklesi hierarchiaban melyik class method-ja hivodjon meg (Method dispatch)
            //      futasi ideju tipus szamit

            // Single dispatch programozasi nyelvek
            // Dynamic dispatch -- pl js

            x.Drive(a);
            y.Drive(a);
            z.Drive(a);

            x.Drive(b);
            y.Drive(b);
            z.Drive(b);

            x.Drive(c);
            y.Drive(c);
            z.Drive(c);

            Console.ReadKey();
        }
    }
}
