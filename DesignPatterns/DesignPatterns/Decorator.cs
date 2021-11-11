using System;

namespace DesignPatterns
{
    /// <summary>
    /// Add new functionality to an existing object without altering its structure
    /// </summary>
    class Decorator : IDesignPattern
    {
        public void DisplayExample()
        {
            ISandwichComponent sandwich = new Sandwich();
            sandwich = new SandwichWithSalade(sandwich);
            sandwich = new SandwichWithTomate(sandwich);

            Console.WriteLine(sandwich.Make());
        }

        #region Implementation
        interface ISandwichComponent
        {
            string Make();
        }

        class Sandwich : ISandwichComponent
        {
            public string Make()
            {
                return "Sandwich";
            }
        }

        abstract class SandwichDecorator : ISandwichComponent
        {
            protected ISandwichComponent sandwich;

            public SandwichDecorator(ISandwichComponent sandwich)
            {
                this.sandwich = sandwich;
            }

            public virtual string Make()
            {
                return sandwich.Make();
            }
        }

        class SandwichWithSalade : SandwichDecorator
        {
            public SandwichWithSalade(ISandwichComponent sandwich)
                : base(sandwich)
            {
            }

            public override string Make()
            {
                return base.Make() + ", Salade";
            }
        }

        class SandwichWithTomate : SandwichDecorator
        {
            public SandwichWithTomate(ISandwichComponent sandwich)
                : base(sandwich)
            {
            }

            public override string Make()
            {
                return base.Make() + ", Tomate";
            }
        }
        #endregion
    }
}