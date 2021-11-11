using System;

namespace DesignPatterns
{
    /// <summary>
    /// Provide an interface for creating objects in a superclass, but allow subclasses to alter the type of objects that will be created.
    /// </summary>
    class FactoryMethod : IDesignPattern
    {
        public void DisplayExample()
        {
            var context = new Context(new CarFactory());

            context.DisplayTravellingCost(10);

            context.SetTravelerFactory(new BoatFactory());

            context.DisplayTravellingCost(10);
        }

        #region Implementation
        class Context
        {
            private TravelerFactory factory;

            public Context(TravelerFactory factory)
            {
                SetTravelerFactory(factory);
            }

            public void SetTravelerFactory(TravelerFactory factory)
            {
                this.factory = factory;
            }

            public void DisplayTravellingCost(int km)
            {
                factory.DisplayTravellingCost(km);
            }
        }

        abstract class TravelerFactory
        {
            private ITraveler traveler;

            protected abstract ITraveler CreateTraveler();

            public void DisplayTravellingCost(int km)
            {
                ITraveler traveler = GetTraveler();

                int cost = traveler.GetTravellingCost(km);

                Console.WriteLine($"Travelling {km} km by {traveler.Name} costs {cost} euros.");
            }

            /// <summary>
            /// Pooling is optional. It simply illustrates the possibilities of having this abstract factory class
            /// </summary>
            private ITraveler GetTraveler()
            {
                if (traveler is null)
                {
                    traveler = CreateTraveler();
                }

                return traveler;
            }
        }

        class CarFactory : TravelerFactory
        {
            protected override ITraveler CreateTraveler()
            {
                return new Car();
            }
        }

        class BoatFactory : TravelerFactory
        {
            protected override ITraveler CreateTraveler()
            {
                return new Boat();
            }
        }

        interface ITraveler
        {
            string Name { get; }
            int GetTravellingCost(int km);
        }

        class Car : ITraveler
        {
            public string Name => "Car";

            public int GetTravellingCost(int km)
            {
                return km * 5;
            }
        }

        class Boat : ITraveler
        {
            public string Name => "Boat";

            public int GetTravellingCost(int km)
            {
                return km * 15;
            }
        }
        #endregion
    }
}