using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTraining
{
    public class HotelSzoba
    {
        public IKonnektor Konnektor;

        public HotelSzoba()
        {
            // Wrong, since HotelSzoba depends on other concrete objects
            // if a new object is implemented, HotelSzoba needs to be adapted
            // breaks SOLID
                // S - Single responsibility (HotelSzoba - creation, usage)
                // D - Higher components should not depend on lower, all components should not depend on implementation
            // new Televizio();
            // new EjjeliLampa();
        }

        public void AramotKapcsol()
        {
            Konnektor?.Mukodik();
        }
    }
    
    public interface IKonnektor
    {
        void Mukodik();
    }

    public class Televizio : IKonnektor
    {
        public void Mukodik()
        {
            Console.WriteLine("Hirado");
        }
    }

    public class EjjeliLampa : IKonnektor
    {
        public void Mukodik()
        {
            Console.WriteLine("Világít");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var hotelSzoba = new HotelSzoba();
            var tv = new Televizio();
            var lampa = new EjjeliLampa();

            hotelSzoba.Konnektor = tv;
            hotelSzoba.AramotKapcsol();
            Console.ReadLine();
        }
    }
}
