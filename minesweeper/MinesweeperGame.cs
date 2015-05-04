using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MinesweeperProject
{
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











