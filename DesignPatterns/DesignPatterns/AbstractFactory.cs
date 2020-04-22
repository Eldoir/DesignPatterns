using System;

namespace DesignPatterns
{
    /// <summary>
    /// Produce families of related objects without specifying their concrete classes.
    /// </summary>
    class AbstractFactory : IDesignPattern
    {
        public void DisplayExample()
        {
            Context context = new Context(new ModernFactory());

            context.DisplayProducts();

            context.SetFactory(new VictorianFactory());

            context.DisplayProducts();
        }

        #region Implementation
        class Context
        {
            private IFactory factory;

            public Context(IFactory factory)
            {
                SetFactory(factory);
            }

            public void SetFactory(IFactory factory)
            {
                this.factory = factory;
            }

            public void DisplayProducts()
            {
                IChair chair = factory.CreateChair();
                Console.WriteLine($"{chair.Name}, has legs: {chair.HasLegs}");

                ISofa sofa = factory.CreateSofa();
                Console.WriteLine($"{sofa.Name}, can sit on: {sofa.CanSitOn}");
            }
        }

        interface IChair
        {
            string Name { get; }
            bool HasLegs { get; }
        }

        interface ISofa
        {
            string Name { get; }
            bool CanSitOn { get; }
        }

        class ModernChair : IChair
        {
            public string Name => "Modern chair";
            public bool HasLegs => false;
        }

        class VictorianChair : IChair
        {
            public string Name => "Victorian chair";
            public bool HasLegs => true;
        }

        class ModernSofa : ISofa
        {
            public string Name => "Modern Sofa";
            public bool CanSitOn => true;
        }

        class VictorianSofa : ISofa
        {
            public string Name => "Victorian Sofa";
            public bool CanSitOn => false;
        }

        interface IFactory
        {
            IChair CreateChair();
            ISofa CreateSofa();
        }

        class ModernFactory : IFactory
        {
            public IChair CreateChair()
            {
                return new ModernChair();
            }

            public ISofa CreateSofa()
            {
                return new ModernSofa();
            }
        }

        class VictorianFactory : IFactory
        {
            public IChair CreateChair()
            {
                return new VictorianChair();
            }

            public ISofa CreateSofa()
            {
                return new VictorianSofa();
            }
        }
        #endregion
    }
}