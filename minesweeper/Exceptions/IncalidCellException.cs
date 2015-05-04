namespace MinesweeperProject.Exceptions
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
}
