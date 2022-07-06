using System;

namespace DesignPatterns
{
    class Program
    {
        static void Main()
        {
            IDesignPattern pattern = new Visitor();
            pattern.DisplayExample();
            Console.Read(); // Avoid quitting instantly
        }
    }
}