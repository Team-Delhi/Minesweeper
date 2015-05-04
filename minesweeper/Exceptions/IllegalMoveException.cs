namespace MinesweeperProject.Exceptions
{
    using System;

    public class IllegalMoveException : Exception
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
