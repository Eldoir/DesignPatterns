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
            var knight = new Unit();

            MoveUp(knight);
            MoveUp(knight);
            MoveUp(knight);

            Undo();

            MoveDown(knight);
        }

        void MoveUp(Unit unit)
        {
            Console.Write("Moving Up: ");
            new MoveUnitCommand(unit, unit.X, unit.Y + 1).Execute();
        }

        void MoveDown(Unit unit)
        {
            Console.Write("Moving Down: ");
            new MoveUnitCommand(unit, unit.X, unit.Y - 1).Execute();
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

                beforeX = unit.X;
                beforeY = unit.Y;
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
            public int X { get; private set; }
            public int Y { get; private set; }

            public Unit()
            {
                Console.WriteLine($"Starting at ({X}, {Y})");
            }

            public void MoveTo(int x, int y)
            {
                X = x;
                Y = y;
                Console.WriteLine($"Moving to ({X}, {Y})");
            }
        }

        static class CommandHistory
        {
            private static readonly Stack<ICommand> commands = new Stack<ICommand>();

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