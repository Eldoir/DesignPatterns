using System;

namespace DesignPatterns
{
    /// <summary>
    /// Ensure a class has one instance, and provide a global point of access to it.
    /// </summary>
    class Singleton : IDesignPattern
    {
        public void DisplayExample()
        {
            SingletonImpl.Instance.DisplayMessage();
        }

        #region Implementation
        class SingletonImpl
        {
            private static SingletonImpl _instance;

            public static SingletonImpl Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new SingletonImpl();
                    }

                    return _instance;
                }
            }

            public void DisplayMessage()
            {
                Console.WriteLine("This is a message from Singleton.");
            }
        }
        #endregion
    }
}