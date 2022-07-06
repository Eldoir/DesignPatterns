using System;

namespace DesignPatterns
{
    /// <summary>
    /// Define new operations without changing the classes of the elements on which they operate.
    /// </summary>
    class Visitor : IDesignPattern
    {
        public void DisplayExample()
        {
            var circle = new Circle();
            var rectangle = new Rectangle();

            var visitor = new LoggerShapeVisitor();
            visitor.Visit(circle);
            visitor.Visit(rectangle);
        }

        #region Implementation
        interface IShape
        {
            void Accept(IShapeVisitor visitor);
        }

        interface IShapeVisitor
        {
            void Visit(Circle c);
            void Visit(Rectangle r);
        }

        class Circle : IShape
        {
            public int Radius => 4;
            public void Accept(IShapeVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        class Rectangle : IShape
        {
            public int Width => 3;
            public int Height => 5;
            public void Accept(IShapeVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        class LoggerShapeVisitor : IShapeVisitor
        {
            public void Visit(Circle c)
            {
                Console.WriteLine($"Circle has radius: {c.Radius}");
            }

            public void Visit(Rectangle r)
            {
                Console.WriteLine($"Rectangle has dimensions: {r.Width} * {r.Height}");
            }
        }
        #endregion
    }
}
