using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    //public class MyClass2
    //{
    //    protected MyClass2(MyClass2 source)
    //    {
    //        this.MyProperty = source.MyProperty;
    //    }

    //    public MyClass2 Clone()
    //    {
    //        return new MyClass2(this);
    //    }

    //    public int MyProperty { get; set; }
    //}

    public class MyClass
    {
        public string Valami { get; private set; }

        //public MyClass2 MyProperty { get; set; }

        public MyClass()
        {
            // draga inicializacios logika
            // ha vannak uj myClass peldanyaim, a Valami erteke ugyanaz lesz
            for (int i = 0; i < 10; i++)
            {
                Valami = Valami + Environment.MachineName.Substring(i);
            }
        }

        //public MyClass(MyClass source)
        //{
        //    this.Valami = source.Valami;
        //}

        public MyClass(MyClass source)
        {
            Valami = source.Valami;
            //MyProperty = source.MyProperty.Clone();
        }

        public MyClass Clone()
        {
            return new MyClass(this);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var c = new MyClass();
            Console.WriteLine(c.Valami);

            var c2 = new MyClass(c);
            Console.WriteLine(c2.Valami);

            var c3 = c.Clone();
            Console.WriteLine(c3.Valami);

            Console.ReadLine();
        }
    }
}
