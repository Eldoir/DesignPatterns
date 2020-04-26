using System;

namespace DesignPatterns
{
    /// <summary>
    /// Pass requests along a chain of handlers. Upon receiving a request, each handler decides either to process the request or to pass it to the next handler in the chain.
    /// </summary>
    class ChainOfResponsibility : IDesignPattern
    {
        public void DisplayExample()
        {
            var monkey = new MonkeyHandler();
            var squirrel = new SquirrelHandler();
            var dog = new DogHandler();

            monkey.SetNext(squirrel).SetNext(dog);
            squirrel.SetNext(dog);

            Console.WriteLine("Chain: Monkey > Squirrel > Dog");
            OfferFood("Banana", monkey);
            OfferFood("Nut", monkey);
            OfferFood("MeatBall", monkey);
            OfferFood("Cup of coffee", monkey);

            Console.WriteLine("\nChain: Squirrel > Dog");
            OfferFood("Banana", squirrel);
        }

        void OfferFood(string foodName, AnimalHandler handler)
        {
            Console.WriteLine($"- Who wants a {foodName}?");

            var result = handler.Handle(foodName);

            if (!string.IsNullOrEmpty(result))
            {
                Console.WriteLine($"\t{result}");
            }
            else
            {
                Console.WriteLine($"\t{foodName} was left untouched.");
            }
        }

        #region Implementation
        interface IHandler<T>
        {
            IHandler<T> SetNext(IHandler<T> handler);
            T Handle(T request);
        }

        abstract class AbstractHandler<T> : IHandler<T>
        {
            protected IHandler<T> nextHandler;

            public IHandler<T> SetNext(IHandler<T> handler)
            {
                nextHandler = handler;
                return handler;
            }

            public virtual T Handle(T request)
            {
                if (nextHandler != null)
                {
                    return nextHandler.Handle(request);
                }
                else
                {
                    return default(T);
                }
            }
        }

        abstract class AnimalHandler : AbstractHandler<string>
        {
            public abstract string name { get; }
            public abstract string foodName { get; }

            public override string Handle(string request)
            {
                if (request.Equals(foodName))
                {
                    return $"{name}: I'll eat the {foodName}.";
                }
                else
                {
                    return base.Handle(request);
                }
            }
        }
        
        class MonkeyHandler : AnimalHandler
        {
            public override string name => "Monkey";
            public override string foodName => "Banana";
        }

        class SquirrelHandler : AnimalHandler
        {
            public override string name => "Squirrel";
            public override string foodName => "Nut";
        }

        class DogHandler : AnimalHandler
        {
            public override string name => "Dog";
            public override string foodName => "MeatBall";
        }
        #endregion
    }
}