using System;

namespace DesignPatterns
{
    /// <summary>
    /// Split a large class or a set of closely related classes
    /// into two separate hierarchies —abstraction and implementation—
    /// which can be developed independently of each other.
    /// </summary>
    class Bridge : IDesignPattern
    {
        public void DisplayExample()
        {
            var remote = new Remote(new TV());
            remote.TurnOnAndSetVolume(5);
            remote.SetDevice(new Radio());
            remote.TurnOnAndSetVolume(10);
        }

        #region Implementation
        class Remote
        {
            private IDevice device;

            public Remote(IDevice device)
            {
                SetDevice(device);
            }

            public void SetDevice(IDevice device)
            {
                this.device = device;
            }

            public void TurnOnAndSetVolume(int volume)
            {
                device.TurnOn();
                device.SetVolume(volume);
            }
        }

        interface IDevice
        {
            void TurnOn();
            void SetVolume(int volume);
        }

        class TV : IDevice
        {
            public void TurnOn()
            {
                Console.WriteLine("The TV is on");
            }

            public void SetVolume(int volume)
            {
                Console.WriteLine($"The TV volume is {volume}");
            }
        }

        class Radio : IDevice
        {
            public void TurnOn()
            {
                Console.WriteLine("The Radio is on");
            }

            public void SetVolume(int volume)
            {
                Console.WriteLine($"The Radio volume is {volume}");
            }
        }
        #endregion
    }
}
