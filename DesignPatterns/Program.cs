using System;

namespace DesignPatterns
{
    class Program
    {
        static void Main()
        {
            IDesignPattern pattern = new Proxy();
            pattern.DisplayExample();
            Console.Read(); // Avoid quitting instantly
        }
    }
}