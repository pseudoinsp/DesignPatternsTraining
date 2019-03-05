using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    public interface IAggregate
    {
        IIterator CreateIterator();
    }

    public interface IIterator
    {
        int GetCurrent();

        bool MoveNext();

        // void Reset();
    }

    

    public class MySuperIntDataStructure : IAggregate
    {
        private HashSet<int> Elements { get; set; }

        private int currentLength;

        public MySuperIntDataStructure()
        {
            Elements = new HashSet<int>();
            currentLength = 0;
        }

        public void Add(int i)
        {
            if(currentLength > 9)
                throw new InvalidOperationException("Already full");

            Elements.Add(i);
            currentLength++;
        }

        public int Length => currentLength;

        public IIterator CreateIterator()
        {
            return new MySuperIntDataStructureIterator(this);
        }

        // nested class so the GetCurrent() can use the private field in the parent class
        private class MySuperIntDataStructureIterator : IIterator
        {
            private readonly MySuperIntDataStructure _source;
            private int currentElementIndex = -1;

            public MySuperIntDataStructureIterator(MySuperIntDataStructure source)
            {
                _source = source;
            }

            public int GetCurrent() => _source.Elements.ElementAt(currentElementIndex);

            public bool MoveNext()
            {
                if (_source.Length == currentElementIndex + 1)
                    return false;

                currentElementIndex++;
                return true;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var dataStructure = new MySuperIntDataStructure();
            dataStructure.Add(3);
            dataStructure.Add(6);
            dataStructure.Add(19);
            dataStructure.Add(54);

            // wrong -- this logic depends on the inner implementation of the data structure
            //for (int i = 0; i < dataStructure.Length; i++)
            //{
            //    Console.WriteLine(dataStructure.Elements[i]);
            //}

            // not responsible for instantiating the iterator when the structure implements IAggregate
            //var iterator = new MySuperIntDataStructureIterator(dataStructure);
            var iterator = dataStructure.CreateIterator();
            while (iterator.MoveNext())
            {
                Console.WriteLine(iterator.GetCurrent());
            }

            Console.ReadKey();
        }
    }
}
