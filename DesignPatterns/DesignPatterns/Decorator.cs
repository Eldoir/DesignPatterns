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
            SandwichComponent sandwich = new Sandwich();
            sandwich = new SandwichWithSalade(sandwich);
            sandwich = new SandwichWithTomate(sandwich);

            Console.WriteLine(sandwich.Make());
        }

        #region Implementation
        interface SandwichComponent
        {
            string Make();
        }

        class Sandwich : SandwichComponent
        {
            public string Make()
            {
                return "Sandwich";
            }
        }

        abstract class SandwichDecorator : SandwichComponent
        {
            protected SandwichComponent sandwich;

            public SandwichDecorator(SandwichComponent sandwich)
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
            public SandwichWithSalade(SandwichComponent sandwich) : base(sandwich) { }

            public override string Make()
            {
                return base.Make() + ", Salade";
            }
        }

        class SandwichWithTomate : SandwichDecorator
        {
            public SandwichWithTomate(SandwichComponent sandwich) : base(sandwich) { }

            public override string Make()
            {
                return base.Make() + ", Tomate";
            }
        }
        #endregion
    }
}