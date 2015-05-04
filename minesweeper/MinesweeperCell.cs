namespace MinesweeperProject
{
    internal class MinesweeperCell
    {
        /// <summary>
        /// Represents a cell on the minefield.
        /// </summary>
        public MinesweeperCell()
        {
            this.Value = ' ';
            this.Revealed = false;
        }

        /// <summary>
        /// Holds the value that the player sees.
        /// </summary>
        public char VisibleValue
        {
            get
            {
                return this.Revealed ? this.Value : '?';
            }
        }

        /// <summary>
        /// Holds the value of the cell. ' ' by default.
        /// </summary>
        public char Value { get; set; }

        public bool Revealed { get; set; }
    }
}
