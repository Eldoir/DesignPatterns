using System;
using System.Collections.Generic;

namespace DesignPatterns
{
    /// <summary>
    ///  Turn a request into a stand-alone object that contains all information about the request. It lets you queue, log and undo requests.
    /// </summary>
    class Command : IDesignPattern
    {
        public void DisplayExample()
        {
            Unit knight = new Unit();

            MoveUp(knight);
            MoveUp(knight);
            MoveUp(knight);

            Undo();

            MoveDown(knight);
        }

        void MoveUp(Unit unit)
        {
            new MoveUnitCommand(unit, unit.x, unit.y + 1).Execute();
        }

        void MoveDown(Unit unit)
        {
            new MoveUnitCommand(unit, unit.x, unit.y - 1).Execute();
        }

        void Undo()
        {
            new UndoCommand().Execute();
        }

        #region Implementation
        interface ICommand
        {
            void Execute();
            void Undo();
        }

        abstract class RegisterableCommand : ICommand
        {
            public virtual void Execute()
            {
                CommandHistory.AddCommand(this);
            }

            public abstract void Undo();
        }

        class MoveUnitCommand : RegisterableCommand
        {
            private Unit unit;
            private int x, y;
            private int beforeX, beforeY;

            public MoveUnitCommand(Unit unit, int x, int y)
            {
                this.unit = unit;
                this.x = x;
                this.y = y;
            }

            public override void Execute()
            {
                base.Execute();

                beforeX = unit.x;
                beforeY = unit.y;
                unit.MoveTo(x, y);
            }

            public override void Undo()
            {
                unit.MoveTo(beforeX, beforeY);
            }
        }

        /// <summary>
        /// You can use this "Null Object" when your methods must return a Command.
        /// </summary>
        class NullCommand : ICommand
        {
            public void Execute() { }
            public void Undo() { }
        }

        class UndoCommand : ICommand
        {
            public void Execute()
            {
                CommandHistory.Undo();
            }

            public void Undo() { }
        }

        class Unit
        {
            public int x { get; private set; }
            public int y { get; private set; }

            public Unit()
            {
                Console.WriteLine($"Starting at ({x}, {y})");
            }

            public void MoveTo(int x, int y)
            {
                this.x = x;
                this.y = y;
                Console.WriteLine($"Moving to ({x}, {y})");
            }
        }

        static class CommandHistory
        {
            private static Stack<ICommand> commands = new Stack<ICommand>();

            public static void AddCommand(ICommand command)
            {
                commands.Push(command);
            }

            public static void Undo()
            {
                if (commands.Count > 0)
                {
                    Console.Write($"Undoing {Utils.GetObjectType(commands.Peek())}: ");
                    commands.Pop().Undo();
                }
                else
                {
                    Console.WriteLine("No command to undo.");
                }
            }
        }

        /// <summary>
        /// Just for the simplicity of logging example.
        /// </summary>
        static class Utils
        {
            public static string GetObjectType(object obj)
            {
                return obj.GetType().ToString().Substring(obj.GetType().ToString().LastIndexOf("+") + 1);
            }
        }
        #endregion
    }
}