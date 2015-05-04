namespace MinesweeperProject
{
    using System;

    internal class InvalidCellException : Exception
    {
        public InvalidCellException()
            : base("Invalid cell!")
        {
        }

        public InvalidCellException(string message)
            : base(message)
        {
        }
    }

    internal class IllegalMoveException : Exception
    {
        public IllegalMoveException()
            : base("Illegal move!")
        {
        }

        public IllegalMoveException(string message)
            : base(message)
        {
        }
    }

    internal class CommandUnknownException : Exception
    {
        public CommandUnknownException()
            : base("Command unknown!")
        {
        }

        public CommandUnknownException(string message)
            : base(message)
        {
        }
    }
}