namespace MinesweeperProject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Exceptions;

    public class MinesweeperGrid
    {
        private MinesweeperCell[,] grid;
        private int minesCount;

        /// <summary>
        /// Represent the mine field.
        /// </summary>
        /// <param name="rows">Amount of rows in the minefield</param>
        /// <param name="columns">Amount of columns in the minefield</param>
        /// <param name="minesCount">Amount of mines on the minefield</param>               
        public MinesweeperGrid(int rows, int columns, int minesCount)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.MinesCount = minesCount;
            this.Grid = new MinesweeperCell[rows, columns];
        }

        /// <summary>
        /// Amount of rows in the minefield.
        /// </summary>
        private int Rows { get; set; }

        /// <summary>
        /// Amount of columns in the minefield.
        /// </summary>
        private int Columns { get; set; }

        /// <summary>
        /// Amount of mines on the minefield.
        /// </summary>
        internal int MinesCount
        { 
            get
            {
                return this.minesCount; 
            }

            set
            {
                if (value <= 0 || this.Rows*this.Columns <= value)
                {
                    throw new ArgumentOutOfRangeException("The number of mines should be at least 1.");
                }
                this.minesCount = value;
            } 
        }

        /// <summary>
        /// A matrice representing the mines on the grid.
        /// </summary>
        private MinesweeperCell[,] Grid
        {
            get
            {
                return this.grid;
            }

            set
            {
                this.grid = value;
                this.FillGrid(this.grid);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   ");

            for (int i = 0; i < this.Columns; i++)
            {
                sb.AppendFormat(" {0}", i);
            }

            sb.Append(" \n");
            sb.Append("   ");
            sb.Append('-', (this.Columns * 2) + 1);
            sb.Append(" \n");

            for (int i = 0; i < this.Rows; i++)
            {
                sb.AppendFormat("{0} |", i);
                for (int j = 0; j < this.Columns; j++)
                {
                    sb.AppendFormat(" {0}", this.grid[i, j].VisibleValue);
                }

                sb.Append(" |\n");
            }

            sb.Append("   ");
            sb.Append('-', (this.Columns * 2) + 1);
            sb.Append(" \n");
            return sb.ToString();
        }

        /// <summary>
        /// Restarts the boar and places new mines
        /// </summary>
        public void RestartBoard()
        {
            this.RestartGrid();
            this.PlaceMines();
        }

        /// <summary>
        /// Reveals all mines on the board.
        /// </summary>
        public void RevealMines()
        {
            foreach (MinesweeperCell mine in this.grid)
            {
                if (mine.Value == '*')
                {
                    mine.Revealed = true;
                }
            }
        }

        /// <summary>
        /// Replaces all fields that are unrevealed and not mines with given marker.
        /// </summary>
        /// <param name="marker">Marker to replace with</param>
        public void MarkUnrevealedMines(char marker)
        {
            foreach (var elem in this.grid)
            {
                if (!elem.Revealed)
                {
                    elem.Revealed = true;
                    if (elem.Value != '*')
                    {
                        elem.Value = marker;
                    }
                }
            }
        }

        /// <summary>
        /// Fills grid with empty fields.
        /// </summary>
        /// <param name="minesGrid">Grid</param>
        public void FillGrid(MinesweeperCell[,] minesGrid)
        {
            var rows = minesGrid.GetLength(0);
            int columns = minesGrid.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    minesGrid[row, column] = new MinesweeperCell();
                }
            }
        }

        /// <summary>
        /// Reveal a cell by given coordinates.
        /// </summary>
        /// <param name="row">X coordinate</param>
        /// <param name="column">Y coordinate</param>
        /// <returns>Cell char value</returns>
        public char RevealCell(int row, int column)
        {
            if ((!this.IsCellOnBoard(row, column)) || this.grid[row, column].Revealed)
            {
                throw new InvalidCellException();
            }

            this.grid[row, column].Revealed = true;

            if (this.grid[row, column].Value != '*')
            {
                int neighbourMinesCount = this.GetNeighbourMinesCount(row, column);
                this.SetCellValue(row, column, neighbourMinesCount.ToString()[0]);
            }

            return this.grid[row, column].Value;
        }

        /// <summary>
        /// Get count of mines positioned around coordinate
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int GetNeighbourMinesCount(int row, int column)
        {
            int count = 0;

            if (!this.IsCellOnBoard(row, column))
            {
                throw new InvalidCellException();
            }
            
            int previousRow = (row - 1) < 0 ? row : row - 1;
            int nextRow = (row + 1) >= this.Rows ? row : row + 1;
            int previousColumn = (column - 1) < 0 ? column : column - 1;
            int nextColumn = (column + 1) >= this.Columns ? column : column + 1;

            for (int i = previousRow; i <= nextRow; i++)
            {
                for (int j = previousColumn; j <= nextColumn; j++)
                {
                    if (this.grid[i, j].Value == '*')
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Check if cell is on board.
        /// </summary>
        /// <param name="row">X coordinate</param>
        /// <param name="column">Y coordinate</param>
        /// <returns>boolean</returns>
        private bool IsCellOnBoard(int row, int column)
        {
            bool onBoard = (row >= 0 && row < this.Rows) && (column >= 0 && column < this.Columns);
            return onBoard;
        }

        /// <summary>
        /// Replace current grid with a new smillar one.
        /// </summary>
        private void RestartGrid()
        {
            this.Grid = new MinesweeperCell[this.Rows, this.Columns];
        }

        /// <summary>
        /// Set value of a cell on given coordinates
        /// </summary>
        /// <param name="row">X coordinate</param>
        /// <param name="column">Y coordinate</param>
        /// <param name="value">New cell value</param>
        private void SetCellValue(int row, int column, char value)
        {
            this.Grid[row, column].Value = value;
        }

        /// <summary>
        /// Put mines on the battleField
        /// </summary>
        private void PlaceMines()
        {
            List<int[]> coordinates = this.GetRandomCoorindates(this.MinesCount, this.Rows, this.Columns);
            coordinates.ForEach(coordinate => this.SetCellValue(coordinate[0], coordinate[1], '*'));
        }

        /// <summary>
        /// Generate a list of random coordinates in given range that don't repeat.
        /// </summary>
        /// <param name="coordinateCount">Amount of coordinates</param>
        /// <param name="rows">Maximum X coordinate</param>
        /// <param name="columns">Maximum Y coordinate</param>
        /// <returns>A list of coordiates (int[]{x,y}})</returns>
        private List<int[]> GetRandomCoorindates(int coordinateCount, int rows, int columns)
        {
            List<int[]> coordinates = new List<int[]>(coordinateCount);
            Random rnd = new Random();
            while (coordinates.Count < coordinateCount)
            {
                int row = rnd.Next(rows);
                int col = rnd.Next(columns);
                int[] coordinate = new int[] { row, col };

                bool coordinateExists = coordinates
                    .Select(coord => coord.SequenceEqual(coordinate))
                    .Any(isContained => isContained);

                if (!coordinateExists)
                {
                    coordinates.Add(new[] { row, col });
                }
            }

            return coordinates;
        }
    }
}