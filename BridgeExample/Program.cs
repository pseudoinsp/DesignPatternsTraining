using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeExample
{
    public abstract class ShapeBase
    {
        private readonly IBrush Brush;

        protected ShapeBase(IBrush brush)
        {
            Brush = brush ?? throw new ArgumentNullException(nameof(brush));
        }

        public void Draw()
        {
            Brush.PaintColor();
            DrawInternal();
        }
        
        protected abstract void DrawInternal();
    }

    public class Circle : ShapeBase
    {
        protected override void DrawInternal() => Console.WriteLine("Körrajz");

        public Circle(IBrush brush) : base(brush)
        {
        }
    }

    public class Triangle : ShapeBase
    {
        protected override void DrawInternal() => Console.WriteLine("3szögrajz");

        public Triangle(IBrush brush) : base(brush)
        {
        }
    }

    public interface IBrush
    {
        void PaintColor();
    }

    class GreenBrush : IBrush
    {
        public void PaintColor() => Console.ForegroundColor = ConsoleColor.Green;
    }

    class RedBrush : IBrush
    {
        public void PaintColor() => Console.ForegroundColor = ConsoleColor.Red;
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Circle(new GreenBrush()).Draw();
            new Circle(new RedBrush()).Draw();
            new Triangle(new RedBrush()).Draw();

            Console.ReadKey();
        }
    }
}
