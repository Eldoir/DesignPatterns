using System;
using System.Collections.Generic;

namespace DesignPatterns
{
    /// <summary>
    /// Save and restore the previous state of an object without revealing the details of its implementation.
    /// </summary>
    class Memento : IDesignPattern
    {
        public void DisplayExample()
        {
            var originator = new Originator();
            var caretaker = new Caretaker(originator);

            caretaker.Save();

            originator.DoSomething();
            originator.DoSomething();

            caretaker.Restore();
        }

        #region Implementation
        class Originator
        {
            private int state;

            public Originator()
            {
                Console.WriteLine($"State created, value: {state}");
            }

            public void DoSomething()
            {
                state++;
                Console.WriteLine($"State incremented, now: {state}");
            }

            public IMemento Save()
            {
                Console.WriteLine($"State saved, value: {state}");
                return new ConcreteMemento(state);
            }

            public void Restore(IMemento memento)
            {
                state = memento.State;
                Console.WriteLine($"State restored, now: {state}");
            }
        }
        
        interface IMemento
        {
            int State { get; }
        }

        class ConcreteMemento : IMemento
        {
            public int State { get; private set; }

            public ConcreteMemento(int state)
            {
                State = state;
            }
        }

        class Caretaker
        {
            private Originator originator;
            private Stack<IMemento> history;

            public Caretaker(Originator originator)
            {
                this.originator = originator;
                history = new Stack<IMemento>();
            }

            public void Save()
            {
                history.Push(originator.Save());
            }

            public void Restore()
            {
                originator.Restore(history.Pop());
            }
        }
        #endregion
    }
}