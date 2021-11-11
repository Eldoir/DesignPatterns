using System;

namespace DesignPatterns
{
    /// <summary>
    /// Improve performance and memory use by reusing objects from a fixed pool instead of allocating and freeing them individually.
    /// </summary>
    class ObjectPool : IDesignPattern
    {
        public void DisplayExample()
        {
            var pool = new ParticlePool();

            int maxLifetime = 10;

            DoFireBurst(pool, maxLifetime);
            Console.WriteLine();

            for (int i = 0; i < maxLifetime; i++)
            {
                Console.WriteLine($"--- Frame {i + 1} ---");
                pool.Animate();
                Console.WriteLine();
            }
        }

        void DoFireBurst(ParticlePool pool, int maxLifetime)
        {
            for (int i = 1; i <= maxLifetime; i++)
            {
                pool.Create(i, i, 1, 1, i);
            }
        }

        #region Implementation
        class ParticlePool
        {
            const int PoolSize = 1000;
            Particle[] particles;
            Particle firstAvailable;

            public ParticlePool()
            {
                particles = new Particle[PoolSize];

                for (int i = 0; i < PoolSize; i++)
                {
                    particles[i] = new Particle();
                }

                firstAvailable = particles[0];

                for (int i = 0; i < PoolSize - 1; i++)
                {
                    particles[i].SetNext(particles[i + 1]);
                }
            }

            public void Create(float x, float y, float xVel, float yVel, int lifetime)
            {
                if (firstAvailable != null)
                {
                    Console.WriteLine($"Creating particle at pos ({x}, {y}) with lifetime {lifetime}");

                    Particle newParticle = firstAvailable;
                    firstAvailable = newParticle.Next;

                    newParticle.Init(x, y, xVel, yVel, lifetime);
                }
                else
                {
                    Console.WriteLine("Can't create any more particles!");
                }
            }

            public void Animate()
            {
                for (int i = 0; i < PoolSize; i++)
                {
                    if (particles[i].Animate())
                    {
                        particles[i].SetNext(firstAvailable);
                        firstAvailable = particles[i];
                    }
                }
            }
        }

        class Particle
        {
            private float x, y;
            private float xVel, yVel;
            private int framesLeft;

            public bool InUse => framesLeft > 0;

            public Particle Next { get; private set; }

            public void Init(float x, float y, float xVel, float yVel, int lifetime)
            {
                this.x = x;
                this.y = y;
                this.xVel = xVel;
                this.yVel = yVel;
                framesLeft = lifetime;
            }

            public void SetNext(Particle next)
            {
                Next = next;
            }

            public bool Animate()
            {
                if (!InUse)
                    return false;

                Console.WriteLine($"Particle animating, frames left: {framesLeft}");

                framesLeft--;
                x += xVel;
                y += yVel;

                if (framesLeft == 0)
                {
                    Console.WriteLine("Particle died");
                }

                return framesLeft == 0;
            }
        }
        #endregion
    }
}