using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperProject
{
    class MinesweeperGrid
    {

        private MinesweeperCell[,] grid;

        public MinesweeperGrid(int rows, int columns, int minesCount)
        {
            Rows = rows;
            Columns = columns;
            MinesCount = minesCount;
            Grid = new MinesweeperCell[rows, columns];
        }

        private int Rows { get; set; }
        private int Columns { get; set; }
        private int MinesCount { get; set; }
        private MinesweeperCell[,] Grid
        {
            get { return grid; }
            set
            {
                grid = value;
                FillGrid(grid, Rows, Columns);
            }
        }

        public void FillGrid(MinesweeperCell[,] minesGrid, int rows, int columns)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    minesGrid[row, column] = new MinesweeperCell();
                }
            }
        }

        public bool IsCellOnBoard(int row, int column)
        {
            bool onBoard = (row >= 0 && row < Rows) && (column >= 0 && column < Columns);
            return onBoard;
        }

        private void SetCellValue(int row, int column, char value)
        {
            if (!IsCellOnBoard(row, column))
            {
                throw new InvalidCellException();
            }

            Grid[row, column].Value = value;
        }

        private char GetCellValue(int row, int column)
        {
            if (!IsCellOnBoard(row, column))
            {
                throw new InvalidCellException();
            }

            char result = grid[row, column].Value;
            return result;
        }

        public char RevealCell(int row, int column)
        {
            if ((!IsCellOnBoard(row, column)) || grid[row, column].Revealed)
            {
                throw new InvalidCellException();
            }

            grid[row, column].Reveal();

            if (grid[row, column].Value != '*')
            {
                int neighbourMinesCount = GetNeighbourMinesCount(row, column);
                SetCellValue(row, column, neighbourMinesCount.ToString()[0]);
            }
            return grid[row, column].Value;
        }

        public int GetNeighbourMinesCount(int row, int column)
        {
            if (!IsCellOnBoard(row, column))
            {
                throw new InvalidCellException();                
            }

            //restrict neigbour cell area
            int previousRow = (row - 1) < 0 ? row : row - 1;
            int nextRow = (row + 1) >= Rows ? row : row + 1;
            int previousColumn = (column - 1) < 0 ? column : column - 1;
            int nextColumn = (column + 1) >= Columns ? column : column + 1; ;

            int count = 0;
            for (int i = previousRow; i <= nextRow; i++)
            {
                for (int j = previousColumn; j <= nextColumn; j++)
                {
                    if (grid[i, j].Value == '*')
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private void PlaceMines()
        {
            List<int[]> coordinates = GetRandomCoorindates(this.MinesCount);
            coordinates.ForEach(coordinate => SetCellValue(coordinate[0], coordinate[1], '*'));
        }

        private List<int[]> GetRandomCoorindates(int minesCount)
        {
            List<int[]>  coordinates = new List<int[]>(minesCount);
            Random rnd = new Random();
            while (coordinates.Count < minesCount)
            {
                int row = rnd.Next(Rows);
                int col = rnd.Next(Columns);
                int[] coordinate = new int[] { row, col };
                if (!CoordinateExists(coordinates, coordinate))
                {
                    coordinates.Add(new[] { row, col });
                }
            }

            return coordinates;
        }

        private bool CoordinateExists(List<int[]> coordinates, int[] coordinate)
        {
            return coordinates.Select(coord => coord.SequenceEqual(coordinate)).Any(isContained => isContained);
        }

        
        public void RestartBoard()
        {
            RestartGrid();
            PlaceMines();
        }

        public void RestartGrid()
        {
            this.Grid = new MinesweeperCell[this.Rows, this.Columns];
        }

        public int RevealedCount()
        {
            int count = 0;
            foreach (MinesweeperCell field in this.Grid)
            {
                if (field.Revealed)
                {
                    count++;
                }
            }
            return count;
        }

        public void RevealMines()
        {
            foreach (MinesweeperCell mine in grid)
            {
                if (mine.Value == '*')
                {
                    mine.Reveal();
                }
            }
        }

        public void MarkUnrevealedMines(char marker)
        {
            foreach (var elem in grid)
            {
                if ((elem.Value != '*') && (!elem.Revealed))
                {
                    elem.Value = marker;
                    elem.Reveal();
                }
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   ");

            //generates column numbers
            for (int i = 0; i < this.Columns; i++)
            {
                sb.AppendFormat(" {0}", i);
            }
            sb.Append(" \n");

            //generates -----------------
            sb.Append("   ");
            sb.Append('-', this.Columns * 2 + 1);
            sb.Append(" \n");

            for (int i = 0; i < this.Rows; i++)
            {
                //generates row number
                sb.AppendFormat("{0} |", i);

                //generate values in each row
                for (int j = 0; j < this.Columns; j++)
                {
                    sb.AppendFormat(" {0}", grid[i, j].VisibleValue);
                }
                sb.Append(" |\n");
            }

            //generates -----------------
            sb.Append("   ");
            sb.Append('-', this.Columns * 2 + 1);
            sb.Append(" \n");

            return sb.ToString();
        }
    }
}
