using System;

namespace DesignPatterns
{
    class Program
    {
        static void Main()
        {
            IDesignPattern pattern = new Bridge();
            pattern.DisplayExample();
            Console.Read(); // Avoid quitting instantly
        }
    }
}