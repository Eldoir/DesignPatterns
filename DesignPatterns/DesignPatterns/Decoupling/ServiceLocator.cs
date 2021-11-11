using System;

namespace DesignPatterns
{
    /// <summary>
    /// Provide a global point of access to a service without coupling users to the concrete class that implements it.
    /// </summary>
    class ServiceLocator : IDesignPattern
    {
        public void DisplayExample()
        {
            Locator.Initialize();

            Locator.AudioService.PlaySound(0);

            Locator.Provide(new ConsoleAudio());

            Locator.AudioService.PlaySound(0);
        }

        #region Implementation
        interface IAudio
        {
            void PlaySound(int soundId);
            void StopSound(int soundId);
            void StopAllSounds();
        }

        class ConsoleAudio : IAudio
        {
            public void PlaySound(int soundId)
            {
                Console.WriteLine($"Play sound with id {soundId}");
            }

            public void StopSound(int soundId)
            {
                Console.WriteLine($"Stop sound with id {soundId}");
            }

            public void StopAllSounds()
            {
                Console.WriteLine("Stop all sounds");
            }
        }

        /// <summary>
        /// By using a Null Object like this, we can ensure the program will never break if a service couldn't be located.
        /// <para>We can add a logging message to indicate that the program now operates on a null object,
        /// in order to facilitate debugging if the non-localization of the service was not voluntary.</para>
        /// </summary>
        class NullAudio : IAudio
        {
            public void PlaySound(int soundId) { LogDebugOutput(); }
            public void StopSound(int soundId) { LogDebugOutput(); }
            public void StopAllSounds() { LogDebugOutput(); }

            void LogDebugOutput() { Console.WriteLine("Using Null Object"); }
        }

        class Locator
        {
            public static IAudio AudioService { get; private set; }
            private static IAudio nullAudioService;

            public static void Initialize()
            {
                nullAudioService = new NullAudio();
                AudioService = nullAudioService;
            }

            public static void Provide(IAudio audio)
            {
                AudioService = audio ?? nullAudioService;
            }
        }
        #endregion
    }
}