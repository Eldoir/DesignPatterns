using System;
using System.Collections.Generic;

namespace DesignPatterns
{
    /// <summary>
    /// Specify the kinds of objects to create using a prototypical instance, and create new objects by copying this prototype.
    /// </summary>
    class Prototype : IDesignPattern
    {
        public void DisplayExample()
        {
            Monster ghost = new Ghost(5, 2);

            var ghostSpawner = new MonsterSpawner(ghost);

            ghostSpawner.Clone();
            ghostSpawner.Clone();

            // You can make use of a registry to add and get ready-to-use prototypes.
            var monsterRegistry = new MonsterRegistry();

            monsterRegistry["BabySkeleton"] = new Skeleton(2, 3);
            monsterRegistry["AdultSkeleton"] = new Skeleton(6, 4);

            var adultSkeleton = (Monster)monsterRegistry["AdultSkeleton"].Clone();

            var skeletonSpawner = new MonsterSpawner(adultSkeleton);

            skeletonSpawner.Clone();
            skeletonSpawner.Clone();
        }

        #region Implementation
        interface IPrototype
        {
            IPrototype Clone();
        }

        class MonsterSpawner
        {
            private Monster prototype;

            public MonsterSpawner(Monster prototype)
            {
                this.prototype = prototype;
            }

            public Monster Clone()
            {
                Monster clonedMonster = (Monster)prototype.Clone();
                Console.WriteLine($"Cloned Monster: {clonedMonster}");
                return clonedMonster;
            }
        }

        abstract class Monster : IPrototype
        {
            protected int health;
            protected int speed;

            public Monster(int health, int speed)
            {
                this.health = health;
                this.speed = speed;
            }

            public abstract IPrototype Clone();
            public abstract string Name { get; }

            public override string ToString()
            {
                return $"Name: {Name}, Health: {health}, Speed: {speed}";
            }
        }

        class Ghost : Monster
        {
            public Ghost(int health, int speed)
                : base(health, speed)
            {
            }

            public override string Name => "Ghost";

            public override IPrototype Clone()
            {
                return new Ghost(health, speed);
            }
        }

        class Skeleton : Monster
        {
            public Skeleton(int health, int speed)
                : base(health, speed)
            {
            }

            public override string Name => "Skeleton";

            public override IPrototype Clone()
            {
                return new Skeleton(health, speed);
            }
        }

        /// <summary>
        /// This class is optional. It can be used to add and get ready-to-use prototypes.
        /// </summary>
        class MonsterRegistry
        {
            private readonly Dictionary<string, IPrototype> monsters = new Dictionary<string, IPrototype>();
            
            public IPrototype this[string key]
            {
                get => monsters[key];
                set => monsters.Add(key, value);
            }
        }
        #endregion
    }
}