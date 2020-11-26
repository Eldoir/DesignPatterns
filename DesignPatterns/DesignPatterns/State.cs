using System;

namespace DesignPatterns
{
    /// <summary>
    /// Allow an object to alter its behavior when its internal state changes. The object will appear to change its class.
    /// </summary>
    class State : IDesignPattern
    {
        public void DisplayExample()
        {
            Hero hero = new Hero();

            hero.SimulatePressJumpButton();
            hero.SimulatePressDuckButton();

            while (true) // Simulate game loop
            {
                hero.Update();
            }
        }

        #region Implementation
        class Hero
        {
            HeroState state;

            public Hero()
            {
                ChangeState(new IdleState());
            }

            public void Update()
            {
                state.Update();
            }

            public void SimulatePressJumpButton()
            {
                ChangeState(new JumpingState());
            }

            public void SimulatePressDuckButton()
            {
                ChangeState(new DuckingState());
            }

            public void ThrowSuperBomb()
            {
                Console.WriteLine("Throwing super bomb!");
            }

            public void ChangeState(HeroState state)
            {
                if (this.state != null)
                    this.state.Exit();

                this.state = state;
                this.state.SetHero(this);

                this.state.Enter();
            }
        }

        abstract class HeroState
        {
            protected Hero hero;

            public void SetHero(Hero hero)
            {
                this.hero = hero;
            }

            public virtual void Enter() { }
            public virtual void Exit() { }

            public virtual void Update() { }
        }

        class IdleState : HeroState
        {
            public override void Enter()
            {
                Console.WriteLine("Entering Idle");
            }
        }

        class JumpingState : HeroState
        {
            public override void Enter()
            {
                Console.WriteLine("Entering Jumping");
            }
        }

        class DuckingState : HeroState
        {
            private int chargeTime = 0;
            private const int MaxChargeTime = 200000000;

            public override void Enter()
            {
                Console.WriteLine("Entering Ducking");
            }

            public override void Update()
            {
                chargeTime++;

                if (chargeTime > MaxChargeTime)
                {
                    hero.ThrowSuperBomb();
                    chargeTime = 0;
                    hero.ChangeState(new IdleState());
                }
            }
        }

        #endregion
    }
}
