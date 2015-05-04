using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperProject
{
    class MinesweeperGrid
    {
        readonly MinesweeperCell[,] grid;
        private readonly int rows;
        private readonly int columns;
        private readonly int minesCount;

        public MinesweeperGrid(int rows, int columns, int minesCount)
        {
            this.rows = rows;
            this.columns = columns;
            this.minesCount = minesCount;
            this.grid = new MinesweeperCell[rows, columns];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    grid[i, j] = new MinesweeperCell();
                }
            }


        }

        /*
         * methods
         */

        public bool IsValidCell(int row, int column)
        {
            return (row >= 0 && row < rows) && (column >= 0 && column < columns);
        }

        private void SetCellValue(int row, int column, char value)
        {
            if (!IsValidCell(row, column))
                throw new InvalidCellException();

            grid[row, column].Value = value;
        }

        private char GetCellValue(int row, int column)
        {
            if (!IsValidCell(row, column))
                throw new InvalidCellException();

            var result = grid[row, column].Value;
            return result;



        }

        public char RevealCell(int row, int column)
        {
            if ((!IsValidCell(row, column)) || grid[row, column].Revealed)
                throw new InvalidCellException();

            grid[row, column].Reveal();
            if (grid[row, column].Value == '*') return grid[row, column].Value;
            var neighbourMinesCount = NeighbourMinesCount(row, column);
            SetCellValue(row, column, neighbourMinesCount.ToString()[0]);
            return grid[row, column].Value;
        }

        public int NeighbourMinesCount(int row, int column)
        {
            if (!IsValidCell(row, column))
                throw new InvalidCellException();

            //restrict neigbour cell area
            var minRow = (row - 1) < 0 ? row : row - 1;
            var maxRow = (row + 1) >= rows ? row : row + 1;
            var minColumn = (column - 1) < 0 ? column : column - 1;
            var maxColumn = (column + 1) >= columns ? column : column + 1; ;



            var count = 0;
            for (var i = minRow; i <= maxRow; i++)
            {
                for (var j = minColumn; j <= maxColumn; j++)
                {
                    if (grid[i, j].Value == '*')
                        count++;
                }
            }
            return count;

        }

        private void Putall()
        {
            var mineCoordinates = new int[minesCount];//creates array of coordinates of mines row*x+column
            var currentMinesCount = 0;



            var randomGenerator = new Random();

            do//generates random coordinates
            {
                var gridCellsCount = rows * columns;//max random number
                var randomNumber = 0;

                do
                {
                    randomNumber = randomGenerator.Next(gridCellsCount);
                }
                while ((mineCoordinates.Count(n => n == randomNumber) > 0));//check if exist

                mineCoordinates[currentMinesCount] = randomNumber;
                currentMinesCount++;
            }
            while (currentMinesCount < minesCount);

            for (var i = 0; i < minesCount; i++)// fill mines
            {



                var row = mineCoordinates[i] / columns;
                var column = mineCoordinates[i] % columns;
                SetCellValue(row, column, '*');
            }
        }

        private void Put()
        {
           for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    var currentCellValue = GetCellValue(i, j);
                    if (currentCellValue == '*') continue;
                    var neighbourMinesCount = NeighbourMinesCount(i, j);
                    SetCellValue(i, j, neighbourMinesCount.ToString()[0]);
                }



            }
        }

        public void Reset()
        {
            reset();
            Putall();
        }

        public void reset()
        {
            foreach (var elem in grid)
            {
                elem.Revealed = false;
                elem.Value = ' ';
            }

        }

        public int RevealedCount()
        {
            var count = 0;
            foreach (var elem in grid)
            {
                if (elem.Revealed)
                    count++;
            }
            return count;
        }

        public void RevealMines()
        {
            foreach (var elem in grid)
            {
                if (elem.Value == '*')
                {
                    elem.Reveal();
                }


            }
        }

        public void Mark(char marker)
        {
            foreach (var elem in grid)
            {
                if ((elem.Value == '*') || (elem.Revealed)) continue;
                elem.Value = marker;
                elem.Reveal();
            }
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("   ");

            //generates column numbers
            for (var i = 0; i < columns; i++)
            {
                sb.AppendFormat(" {0}", i);
            }
            sb.Append(" \n");

            //generates -----------------
            sb.Append("   ");
            sb.Append('-', columns * 2 + 1);
            sb.Append(" \n");

            for (var i = 0; i < rows; i++)
            {
                //generates row number
                sb.AppendFormat("{0} |", i);

                //generate values in each row
                for (var j = 0; j < columns; j++)
                {
                    sb.AppendFormat(" {0}", grid[i, j].VisibleValue);
                }
                sb.Append(" |\n");
            }

            //generates -----------------
            sb.Append("   ");
            sb.Append('-', columns * 2 + 1);
            sb.Append(" \n");

            return sb.ToString();
        }
        public MinesweeperCell Get(int row, int column)
        {
            if (IsValidCell(row, column))
                throw new InvalidCellException();
            return grid[row, column];
        }
    }
}
