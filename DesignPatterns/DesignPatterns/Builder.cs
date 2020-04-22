using System;

namespace DesignPatterns
{
    /// <summary>
    /// Construct complex objects step by step. The pattern allows you to produce different types
    /// and representations of an object using the same construction code.
    /// </summary>
    class Builder : IDesignPattern
    {
        public void DisplayExample()
        {
            CarBuilder builder = new CarBuilder();

            CarDirector director = new CarDirector(builder);

            director.BuildCheapCar();
            builder.GetResult().DisplayOptions();

            Console.WriteLine();

            director.BuildSportsCar();
            builder.GetResult().DisplayOptions();
        }

        #region Implementation
        class CarDirector
        {
            IVehicleBuilder builder;

            public CarDirector(IVehicleBuilder builder)
            {
                SetBuilder(builder);
            }

            public void SetBuilder(IVehicleBuilder builder)
            {
                this.builder = builder;
            }

            public void BuildCheapCar()
            {
                Console.WriteLine("--- Building a cheap car ---");
                builder.Reset();
                builder.SetSeats(4);
                builder.SetGPS();
            }

            public void BuildSportsCar()
            {
                Console.WriteLine("--- Building a sports car ---");
                builder.Reset();
                builder.SetSeats(2);
                builder.SetGPS();
                builder.SetClim();
            }
        }

        interface IVehicleBuilder
        {
            void Reset();
            void SetSeats(int n);
            void SetGPS();
            void SetClim();
        }

        class CarBuilder : IVehicleBuilder
        {
            private Car car;

            public void Reset()
            {
                car = new Car();
            }

            public void SetSeats(int n)
            {
                car.seats = n;
            }

            public void SetGPS()
            {
                car.gps = true;
            }

            public void SetClim()
            {
                car.clim = true;
            }

            public Car GetResult()
            {
                Car resultCar = car;
                Reset(); // Be ready to build another car
                return resultCar;
            }
        }

        class Car
        {
            public int seats;
            public bool gps;
            public bool clim;

            public void DisplayOptions()
            {
                string result = "This car has: ";

                result += $"{seats} seats";

                if (gps)
                    result += ", GPS";

                if (clim)
                    result += ", clim";

                Console.WriteLine(result);
            }
        }
        #endregion
    }
}