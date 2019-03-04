using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDecorator
{
    public abstract class Stream
    {
        public abstract void Write(int[] input);
    }

    public class FileStream : Stream
    {
        public override void Write(int[] input)
        {
            Console.WriteLine("Writing input to file");
            Console.WriteLine(string.Join(", ", input));
        }
    }

    public class MemoryStream : Stream
    {
        public override void Write(int[] input)
        {
            Console.WriteLine("Writing input to memory");
            Console.WriteLine(string.Join(", ", input));
        }
    }

    public abstract class DecoratorBase : Stream
    {
        // instead of protected and no Write overload
        private readonly Stream Decoratee;

        protected DecoratorBase(Stream decoratee)
        {
            Decoratee = decoratee;
        }

        public override void Write(int[] input)
        {
            Decoratee.Write(input);
        }
    }

    public class ZipStream : DecoratorBase
    {
        public ZipStream(Stream decoratee) : base(decoratee)
        {
        }

        public override void Write(int[] input)
        {
            var zippedData = input.Take(5).ToArray();
            base.Write(zippedData);
        }
    }

    public class CryptoStream : DecoratorBase
    {
        public CryptoStream(Stream decoratee) : base(decoratee)
        {
        }

        public override void Write(int[] input)
        {
            var encryptedData = input.Select(i => i + 1).ToArray();
            base.Write(encryptedData);
        }
    }


    // Wrong -- tied to FileStream
    //public class ZipFileStream : FileStream
    //{
    //    public override void Write(int[] input)
    //    {
    //        var zippedData = input.Take(5).ToArray();
    //        base.Write(zippedData);
    //    }
    //}

    //public class CryptoFileStream : FileStream
    //{
    //    public override void Write(int[] input)
    //    {
    //        var encryptedData = input.Select(i => i+1).ToArray();
    //        base.Write(encryptedData);
    //    }
    //}


    class Program
    {
        static void Main(string[] args)
        {
            //ZipStream zs = new ZipStream(new FileStream());
            //zs.Write(new[] { 1, 2, 3, 4, 5, 6 });

            var zcs = new ZipStream(new CryptoStream(new MemoryStream()));
            zcs.Write(new[] { 1, 2, 3, 4, 5, 6, 7, 8 });

            Console.ReadKey();
        }
    }
}
