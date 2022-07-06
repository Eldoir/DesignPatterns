using System;

namespace DesignPatterns
{
    /// <summary>
    /// Provide a placeholder for another object to control access to it.
    /// </summary>
    class Proxy : IDesignPattern
    {
        public void DisplayExample()
        {
            ISubject realSubject = new RealSubject();
            realSubject.DoStuff();

            Console.WriteLine("---------------------");

            ISubject proxySubject = new ProxySubject(realSubject);
            proxySubject.DoStuff();
        }

        #region Implementation
        interface ISubject
        {
            void DoStuff();
        }

        class RealSubject : ISubject
        {
            public void DoStuff()
            {
                Console.WriteLine("'I live freely!'");
            }
        }

        class ProxySubject : ISubject
        {
            private ISubject subject;

            public ProxySubject(ISubject subject)
            {
                this.subject = subject;
            }

            public void DoStuff()
            {
                Console.WriteLine("Let me check something first.");
                subject.DoStuff();
                Console.WriteLine("Ok, you can go now.");
            }
        }
        #endregion
    }
}
