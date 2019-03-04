﻿using System;
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

    public class Eloszto : IKonnektor
    {
        private readonly List<IKonnektor> _konnektors = new List<IKonnektor>();

        public List<IKonnektor> Konnektors => _konnektors;

        public void Mukodik()
        {
            foreach (var konnektor in _konnektors)
            {
                konnektor.Mukodik();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var hotelSzoba = new HotelSzoba();
            var tv = new Televizio();
            var lampa = new EjjeliLampa();
            var eloszto = new Eloszto();

            eloszto.Konnektors.AddRange(new IKonnektor[] { tv, lampa });

            // Elosztokat is tudok dugni elosztokba -> Composite pattern
            // Ezert a composite-nak ugyanazt az interfeszt kell tamogatni mint amiket tartalmaz

            hotelSzoba.Konnektor = eloszto;
            hotelSzoba.AramotKapcsol();

            Console.ReadLine();
        }
    }
}
