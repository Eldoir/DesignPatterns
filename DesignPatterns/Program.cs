﻿using System;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            IDesignPattern pattern = new State();
            pattern.DisplayExample();
            Console.Read(); // Avoid quitting instantly
        }
    }
}