using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Context
    {
        public string Input { get; set; }
        public int Output { get; set; }
    }

    public abstract class Expression
    {
        // Ezekkel pl
        // MCMXXVII
        // 1|
        // mapping: ThosandE ->... -> OneE
        // majd Nine, Four, Five, One
        //One ciklikusan mivel abbol tobb is lehet
        public void Interpret(Context context)
        {
            if (context.Input.StartsWith(Nine()))
            {
                context.Output += 9 * Multiplier();
                context.Input = context.Input.Substring(2);
            }
            else if (context.Input.StartsWith(Four()))
            {
                context.Output += 4 * Multiplier();
                context.Input = context.Input.Substring(2);
            }
            else if (context.Input.StartsWith(Five()))
            {
                context.Output += 5 * Multiplier();
                context.Input = context.Input.Substring(1);
            }

            while (context.Input.StartsWith(One()))
            {
                context.Output += 1 * Multiplier();
                context.Input = context.Input.Substring(1);
            }
        }

        public abstract string One();
        public abstract string Four();
        public abstract string Five();
        public abstract string Nine();

        public abstract int Multiplier();
    }

    // Romai: One Expression:  Multiplier: 1, One -> I, Four -> IV, ...
    //        Ten E         : M: 10, One -> X, Four -> XL, ...
    //        Hundred E     : M: 100, One -> C, Nine -> CM, ....
    //        Thousand E    : M: 1000, One -> M, ...
    //  Most fiktiven minden helyett lesz egy terminális szimbolum

    public class OneExpression : Expression
    {
        public override string One() => "I";

        public override string Four() => "IV";

        public override string Five() => "V";

        public override string Nine() => "IX";

        public override int Multiplier() => 1;
    }

    public class TenExpression : Expression
    {
        public override string One() => "X";

        public override string Four() => "XL";

        public override string Five() => "L";

        public override string Nine() => "XC";

        public override int Multiplier() => 10;
    }

    public class HundredExpression : Expression
    {
        public override string One() => "C";

        public override string Four() => "CD";

        public override string Five() => "D";

        public override string Nine() => "CM";

        public override int Multiplier() => 100;
    }

    public class ThousandExpression : Expression
    {
        public override string One() => "M";

        public override string Four() => "&";

        public override string Five() => "&";

        public override string Nine() => "&";

        public override int Multiplier() => 1000;
    }


    class Program
    {
        static void Main(string[] args)
        {
            // abstract syntax tree
            List<Expression> ast = new List<Expression>()
            {
                new ThousandExpression(),
                new HundredExpression(),
                new TenExpression(),
                new OneExpression()
            };

            var context = new Context() { Input = "MCMXXVIII"};

            foreach (var expression in ast)
            {
                expression.Interpret(context);
            }

            Console.WriteLine(context.Output);

            Console.ReadKey();
        }
    }
}
