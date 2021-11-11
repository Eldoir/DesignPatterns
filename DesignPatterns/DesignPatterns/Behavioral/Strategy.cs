using System;
using System.Linq;

namespace DesignPatterns
{
    /// <summary>
    /// Define a family of algorithms, put each of them in a separate class, and make their objects interchangeable.
    /// </summary>
    class Strategy : IDesignPattern
    {
        public void DisplayExample()
        {
            var context = new Context(new OrderAscendingStrategy());

            var nbs = new [] { 2, 6, 4, 7, 1, 9 };

            context.DisplaySorted(nbs);

            context.SetStrategy(new OrderDescendingStrategy());

            context.DisplaySorted(nbs);
        }

        #region Implementation
        class Context
        {
            private ISortingStrategy strategy;

            public Context(ISortingStrategy strategy)
            {
                SetStrategy(strategy);
            }

            public void SetStrategy(ISortingStrategy strategy)
            {
                this.strategy = strategy;
            }

            public void DisplaySorted(int[] nbs)
            {
                int[] sortedNbs = strategy.Sort(nbs);

                var result = string.Empty;
                foreach (int nb in sortedNbs)
                {
                    result += nb + " ";
                }

                Console.WriteLine(result);
            }
        }

        interface ISortingStrategy
        {
            int[] Sort(int[] nbs);
        }

        class OrderAscendingStrategy : ISortingStrategy
        {
            public int[] Sort(int[] nbs)
            {
                return nbs.OrderBy(n => n).ToArray();
            }
        }

        class OrderDescendingStrategy : ISortingStrategy
        {
            public int[] Sort(int[] nbs)
            {
                return nbs.OrderByDescending(n => n).ToArray();
            }
        }
        #endregion
    }
}