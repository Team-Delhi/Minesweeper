using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MinesweeperProject
{
    class MinesweeperGame
    {
        private readonly MinesweeperGrid grid;

        public List<ScoreRecord> ScoreBoard { get; set; }

        public int Score { get; set; }

        public MinesweeperGrid Grid
        {
            get
            {
                return grid;
            }
        }
        
        public MinesweeperGame(int rows, int columns, int minesCount)
        {
            grid = new MinesweeperGrid(rows, columns, minesCount);
            ScoreBoard = new List<ScoreRecord>();
        }

        public virtual void Start()
        {
            Grid.Reset();
            Score = 0;
        }

    }
}











