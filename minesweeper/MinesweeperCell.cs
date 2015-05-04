namespace MinesweeperProject
{
    class MinesweeperCell
    {
        public MinesweeperCell()
        {
            this.Value = ' ';
            this.Revealed = false;
        }
        public char VisibleValue
        {
            get
            {
                return this.Revealed ? this.Value : '?';
            }
        }
        public char Value { get; set; }

        public bool Revealed { get; set; }
    }
}
