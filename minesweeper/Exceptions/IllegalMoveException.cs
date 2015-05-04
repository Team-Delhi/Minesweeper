namespace MinesweeperProject.Exceptions
{
    using System;

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
}
