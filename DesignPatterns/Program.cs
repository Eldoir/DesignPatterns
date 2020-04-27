using System;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            IDesignPattern pattern = new ObjectPool();
            pattern.DisplayExample();
            Console.Read(); // Avoid quitting instantly
        }
    }
}