namespace MinesweeperProject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Exceptions;

    /// <summary>
    /// This is the engine of the game
    /// </summary>
    public sealed class ConsoleMinesweeperGame
    {
        private const string StartMessage = "Welcome to the game “Minesweeper”. Try to reveal all cells without mines. " +
                                            "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' " +
                                            "to quit the game.";

        private static readonly object SyncLock = new object();

        private static ConsoleMinesweeperGame instance;

        private ConsoleMinesweeperGame(int rows, int columns, int minesCount)
        {
            this.Grid = new MinesweeperGrid(rows, columns, minesCount);
            this.ScoreBoard = new List<ScoreRecord>();
        }

        public List<ScoreRecord> ScoreBoard { get; set; }

        public int Score { get; set; }

        public MinesweeperGrid Grid { get; private set; }

        /// <summary>
        /// The method that creates the only instance of the engine
        /// </summary>
        /// <param name="rows">The rows of the grid</param>
        /// <param name="cols">The cols of the grid</param>
        /// <param name="minesCount">The count of the mines in the game</param>
        /// <returns></returns>
        public static ConsoleMinesweeperGame Instance(int rows, int cols, int minesCount)
        {
                if (instance == null)
                {
                    lock (SyncLock)
                    {
                        if (instance == null)
                        {
                            instance = new ConsoleMinesweeperGame(rows, cols, minesCount);
                        }
                    }
                }

                return instance;
        }

        public void Start()
        {
            this.Grid.RestartBoard();
            this.Score = 0;
            Console.WriteLine(StartMessage);
            Console.WriteLine(this.Grid.ToString());
            this.NextCommand();
        }

        /// <summary>
        /// The method that accepts the commands and processes them
        /// </summary>
        public void NextCommand()
        {
            Console.Write("Enter command or row and column: ");
            
            string commandLine = Console.ReadLine().ToUpper().Trim();

            List<string> commandList = commandLine.Split(' ').ToList();
            
            if (commandList.Count == 0)
            {
                this.NextCommand();
            }

            try
            {
                string firstCommand = commandList.ElementAt(0);
                switch (firstCommand)
                {
                    case "RESTART":
                        this.Start();
                        break;
                    case "TOP":
                        this.PrintScoreBoard();
                        this.NextCommand();
                        break;
                    case "EXIT":
                        this.Exit();
                        break;
                    case "NAKOV":
                        this.Grid.RevealMines();
                        Console.WriteLine(this.Grid.ToString());
                        this.NextCommand();
                        break;
                    default:
                        int row, column;

                        if (commandList.Count < 2)
                        {
                            throw new CommandUnknownException();
                        }

                        var tryParse = int.TryParse(commandList.ElementAt(0), out row);
                        tryParse = int.TryParse(commandList.ElementAt(1), out column) && tryParse;

                        if (!tryParse)
                        {
                            throw new CommandUnknownException();
                        }

                        if (this.Grid.RevealCell(row, column) == '*')
                        {
                            this.Grid.MarkUnrevealedMines('-');
                            this.Grid.RevealMines();
                            Console.WriteLine(this.Grid.ToString());
                            Console.WriteLine(
                                "Booooom! You were killed by a mine. You revealed {0} cells without mines.", this.Score);
                            Console.Write("Please enter your name for the top scoreboard: ");
                            var playerName = Console.ReadLine();
                            var score = new ScoreRecord(playerName, this.Score);
                            this.ScoreBoard.Add(score);
                            Console.WriteLine();
                            this.PrintScoreBoard();
                            this.Start();
                        }
                        else
                        {
                            Console.WriteLine(Grid.ToString());
                            this.Score++;
                            this.NextCommand();
                        }

                        break;
                }
            }
            catch (InvalidCellException)
            {
                Console.WriteLine("Illegal move!");
                this.NextCommand();
            }
            catch (CommandUnknownException)
            {
                Console.WriteLine("Unknown command!");
                this.NextCommand();
            }
        }

        public void Exit()
        {
            Console.WriteLine("Good bye!");
        }

        public void PrintScoreBoard()
        {
            string top = "Scoreboard:\n";            
            var topScore = this.ScoreBoard.OrderByDescending(score => score);
            var count = 0;
            foreach (var score in topScore)
            {
                count++;
                top += string.Format("{0}. {1}", count, score);
                
            }

           Console.Write(top);

        }
    }
}
