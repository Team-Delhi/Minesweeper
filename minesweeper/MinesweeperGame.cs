namespace MinesweeperProject
{
    using System.Collections.Generic;

    class MinesweeperGame
    {
        public List<ScoreRecord> ScoreBoard { get; set; }

        public int Score { get; set; }

        public MinesweeperGrid Grid { get; private set; }

        public MinesweeperGame(int rows, int columns, int minesCount)
        {
            this.Grid = new MinesweeperGrid(rows, columns, minesCount);
            this.ScoreBoard = new List<ScoreRecord>();
        }

        public virtual void Start()
        {
            this.Grid.Reset();
            this.Score = 0;
        }

    }
}