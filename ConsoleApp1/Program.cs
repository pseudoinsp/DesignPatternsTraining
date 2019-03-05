using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Kavezo
    {
        public KavefozoBase Kavefozo { get; set; }

        public void KavetRendel()
        {
            if (Kavefozo == null)
            {
                Console.WriteLine("Nincs kávé");
                return;
            }

            Console.WriteLine("Kersz kavet?");
            string s = Console.ReadLine();
            if(s == "Y")
                Kavefozo.KavetFoz();
        }
    }

    public abstract class KavefozoBase
    {
        protected abstract void VizetForral();
        protected abstract void Daral();
        protected abstract void Lefoz();
        protected abstract void Izesit();

        public void KavetFoz()
        {
            VizetForral();
            Daral();
            Lefoz();
            Izesit();
        }
    }

    public class OlcsoKavefozo : KavefozoBase
    {
        protected override void VizetForral()
        {
            Console.WriteLine("98 fokra forral");
        }

        protected override void Daral()
        {
            Console.WriteLine("Kessel daral");
        }

        protected override void Lefoz()
        {
            Console.WriteLine("Hangosan főz");
        }

        protected override void Izesit()
        {
            Console.WriteLine("Tejport rak bele");
        }
    }

    public class DragaKavefozo : KavefozoBase
    {
        protected override void VizetForral()
        {
            Console.WriteLine("82 fokra forral");
        }

        protected override void Daral()
        {
            Console.WriteLine("Jo kessel daral");
        }

        protected override void Lefoz()
        {
            Console.WriteLine("Halkan főz");
        }

        protected override void Izesit()
        {
            Console.WriteLine("Mandulatejet rak bele");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var kavezo = new Kavezo() {Kavefozo = new OlcsoKavefozo()};

            kavezo.KavetRendel();

            kavezo.Kavefozo = new DragaKavefozo();

            kavezo.KavetRendel();

            Console.ReadKey();
        }
    }
}
