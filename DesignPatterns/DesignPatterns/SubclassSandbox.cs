using System;

namespace DesignPatterns
{
    /// <summary>
    /// Define behavior in a subclass using a set of operations provided by its base class.
    /// </summary>
    class SubclassSandbox : IDesignPattern
    {
        public void DisplayExample()
        {
            var particleSystem = new ParticleSystem();

            Superpower skyLaunch = CreateSkyLaunch(particleSystem);

            Console.WriteLine("--- Activating Sky Launch ---");
            skyLaunch.Activate();

            Superpower iceWall = CreateIceWall(particleSystem);

            Console.WriteLine("--- Activating Ice Wall ---");
            iceWall.Activate();
        }

        Superpower CreateSkyLaunch(ParticleSystem particleSystem)
        {
            return new SkyLaunch().Init(particleSystem);
        }

        Superpower CreateIceWall(ParticleSystem particleSystem)
        {
            return new IceWall().Init(particleSystem);
        }

        #region Implementation
        abstract class Superpower
        {
            private ParticleSystem particleSystem;

            public Superpower Init(ParticleSystem particleSystem)
            {
                this.particleSystem = particleSystem;
                return this; // Allow chaining
            }

            public abstract void Activate();

            protected void Move(float x, float y, float z)
            {
                Console.WriteLine($"Move at pos ({x}, {y}, {z})");
            }

            protected void PlaySound(int soundId, float volume)
            {
                Console.WriteLine($"Play sound with id {soundId} at volume {volume}");
            }

            protected void SpawnParticles(int particleType, int count)
            {
                particleSystem.SpawnParticles(particleType, count);
            }
        }

        class SkyLaunch : Superpower
        {
            public override void Activate()
            {
                Move(0, 10, 0);
                PlaySound(0, 0.5f);
                SpawnParticles(0, 10);
            }
        }

        class IceWall : Superpower
        {
            public override void Activate()
            {
                PlaySound(1, 1f);
                SpawnParticles(1, 100);
            }
        }

        class ParticleSystem
        {
            public void SpawnParticles(int particleType, int count)
            {
                Console.WriteLine($"Spawn {count} particles of type {particleType}");
            }
        }
        #endregion
    }
}