namespace MinesweeperProject.Exceptions
{
    using System;

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
