using System;
using System.Collections.Generic;
using System.Threading;

namespace DesignPatterns
{
    /// <summary>
    /// Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.
    /// </summary>
    class Observer : IDesignPattern
    {
        public void DisplayExample()
        {
            var subject = new Subject();

            var observerA = new ObserverA();
            var observerB = new ObserverB();

            subject.Attach(observerA);
            subject.Attach(observerB);

            subject.TriggerExample();

            subject.Detach(observerA);

            Thread.Sleep(20); // Simulate time

            subject.TriggerExample();
        }

        #region Implementation
        interface IObserver
        {
            void OnNotify(ISubject subject);
        }

        interface ISubject
        {
            void Attach(IObserver observer);
            void Detach(IObserver observer);
            void Notify();
        }

        class Subject : ISubject
        {
            public int State { get; private set; }

            private readonly List<IObserver> observers = new List<IObserver>();

            public void Attach(IObserver observer)
            {
                observers.Add(observer);
            }

            public void Detach(IObserver observer)
            {
                observers.Remove(observer);
            }

            public void Notify()
            {
                foreach (IObserver observer in observers)
                {
                    observer.OnNotify(this);
                }
            }

            public void TriggerExample()
            {
                Console.WriteLine("\n- Triggering Example:\n");
                State = new Random().Next(0, 10);
                Notify();
            }
        }

        class ObserverA : IObserver
        {
            public void OnNotify(ISubject subject)
            {
                Console.WriteLine("ObserverA got notified");
            }
        }

        class ObserverB : IObserver
        {
            public void OnNotify(ISubject subject)
            {
                Console.WriteLine($"ObserverB got notified: subject has State {(subject as Subject).State}");
            }
        }
        #endregion
    }
}