using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memento
{
    // Caretaker: stores list of Mementos, not implemented here

    public interface IMemento
    {
        int CalculatorState { get; }
    }

    public class Memento : IMemento
    {
        public int CalculatorState { get; set; }
    }

    public class Calculator
    {
        private IDictionary<Guid, IMemento> mementos = new Dictionary<Guid, IMemento>();

        public int DisplayNumber { get; private set; }

        public void Add(int a)
        {
            DisplayNumber += a;
        }

        public void Subtract(int a)
        {
            DisplayNumber -= a;
        }

        public Guid CreateMementoFromGuid()
        {
            var m = new Memento() { CalculatorState = DisplayNumber};
            var g = Guid.NewGuid();
            mementos.Add(g, m);
            return g;
        }

        public IMemento CreateMemento()
        {
            return new Memento() { CalculatorState = DisplayNumber };
        }
        
        public void SetFromMemento(IMemento m)
        {
            this.DisplayNumber = m.CalculatorState;
        }

        public void SetFromMementoFromGuid(Guid g)
        {
            var m = mementos[g];
            SetFromMemento(m);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator();

            calculator.Add(3);
            calculator.Add(9);
            calculator.Subtract(3);
            
            // this is the caretaker here
            IMemento m = calculator.CreateMemento();

            calculator.Add(5);
            calculator.Add(10);
            calculator.Add(20);
            
            // ugly because calculator defines public API to modify inner state
            // but memento stores inner state
            // how to guarantee that the memento does not corrupt the data between save and load?
            // -> Solution: IMemento with get only memento

            // but an other problem: 2 calculators, 1-1 memento
            // how to guarantee that only the own mementos are getting loaded?
            // only solution is to store an IDict <Guid, Memento> dictionary in the calculator itself
            // in this case, the caretaker is the originator itself!
            calculator.SetFromMemento(m);
        }
    }
}
