using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Visitor_3
{
    public interface ITreeVisitor
    {
        void Visit(StringNode node);
        void Visit(IntNode node);
    }

    public class PreOrderVisitor : ITreeVisitor
    {
        public void Visit(StringNode node)
        {
            Console.WriteLine($"A string értéke: {node.StringValue}");
            node.Left?.Accept(this);
            node.Right?.Accept(this);
        }

        public void Visit(IntNode node)
        {
            Console.WriteLine($"A string értéke: {node.IntValue}");
            node.Left?.Accept(this);
            node.Right?.Accept(this);
        }
    }

    public class InOrderVisitor : ITreeVisitor
    {
        public void Visit(StringNode node)
        {
            node.Left?.Accept(this);
            Console.WriteLine($"A string értéke: {node.StringValue}");
            node.Right?.Accept(this);
        }

        public void Visit(IntNode node)
        {
            node.Left?.Accept(this);
            Console.WriteLine($"A string értéke: {node.IntValue}");
            node.Right?.Accept(this);
        }
    }

    public class PostOrderVisitor : ITreeVisitor
    {
        public void Visit(StringNode node)
        {
            node.Left?.Accept(this);
            node.Right?.Accept(this);
            Console.WriteLine($"A string értéke: {node.StringValue}");
        }

        public void Visit(IntNode node)
        {
            node.Left?.Accept(this);
            node.Right?.Accept(this);
            Console.WriteLine($"A string értéke: {node.IntValue}");
        }
    }

    public abstract class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }

        public abstract void Accept(ITreeVisitor visitor);
    }

    public class StringNode : Node
    {
        public string StringValue { get; set; }
        public override void Accept(ITreeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class IntNode : Node
    {
        public int IntValue { get; set; }
        public override void Accept(ITreeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            var n = new StringNode()
            {
                StringValue = "root",
                Left = new IntNode()
                {
                    IntValue = 234,
                    Right = new StringNode()
                    {
                        StringValue = "jnsls"
                    }
                    
                },
                Right = new IntNode()
                {
                    IntValue = 21,
                    Right = new StringNode()
                    {
                        StringValue = "nnre"
                    }
                }
            };

            var preOrderVisitor = new PreOrderVisitor();
            n.Accept(preOrderVisitor);
            Console.WriteLine();

            var inorder = new InOrderVisitor();
            n.Accept(inorder);
            Console.WriteLine();

            var postorder = new PostOrderVisitor();
            n.Accept(postorder);


            Console.ReadKey();
        }
    }
}
