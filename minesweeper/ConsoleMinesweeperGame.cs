namespace MinesweeperProject
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    /// <summary>
    /// This is the engine of the game
    /// </summary>
    internal sealed class ConsoleMinesweeperGame
    {
        private const string StartMessage = "Welcome to the game “Minesweeper”. Try to reveal all cells without mines. " +
                                            "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' " +
                                            "to quit the game.";

        private static ConsoleMinesweeperGame instance;
        private static readonly object SyncLock = new object();

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
            Console.WriteLine(Grid.ToString());
            NextCommand();
        }

        /// <summary>
        /// The method that accepts the commands and processes them
        /// </summary>
        public void NextCommand()
        {
            
            Console.Write("Enter command or row and column: ");
            
            var commandLine = Console.ReadLine().ToUpper().Trim();

            var commandList = commandLine.Split(' ').ToList();
            
            if (commandList.Count == 0)
            {
                NextCommand();
            }

            try
            {
                var firstCommand = commandList.ElementAt(0);
                switch (firstCommand)
                {
                    case "RESTART":
                        Start();
                        break;
                    case "TOP":
                        PrintScoreBoard();
                        NextCommand();
                        break;
                    case "EXIT":
                        Exit();
                        break;
                    case "NAKOV":
                        Grid.RevealMines();
                        Console.WriteLine(Grid.ToString());
                        NextCommand();
                        break;
                    default:
                        int row, column;

                        if (commandList.Count < 2)
                        {
                            throw new CommandUnknownException();
                        }

                        var tryParse = (int.TryParse(commandList.ElementAt(0), out row));
                        tryParse = (int.TryParse(commandList.ElementAt(1), out column) && tryParse);

                        if (!tryParse)
                        {
                            throw new CommandUnknownException();
                        }

                        if (Grid.RevealCell(row, column) == '*')
                        {
                            Grid.MarkUnrevealedMines('-');
                            Grid.RevealMines();
                            Console.WriteLine(Grid.ToString());
                            Console.WriteLine(
                                "Booooom! You were killed by a mine. You revealed {0} cells without mines.", Score);
                            Console.Write("Please enter your name for the top scoreboard: ");
                            var playerName = Console.ReadLine();
                            var score = new ScoreRecord(playerName, Score);
                            ScoreBoard.Add(score);
                            Console.WriteLine();
                            PrintScoreBoard();
                            Start();
                        }
                        else
                        {
                            Console.WriteLine(Grid.ToString());
                            Score++;
                            NextCommand();
                        }
                        break;
                }
            }
            catch (InvalidCellException)
            {
                Console.WriteLine("Illegal move!");
                NextCommand();
            }
            catch (CommandUnknownException)
            {
                Console.WriteLine("Unknown command!");
                NextCommand();
            }
        }

        public void Exit()
        {
            Console.WriteLine("Good bye!");
        }

        public void PrintScoreBoard()
        {
            var sb = new StringBuilder();    
            sb.AppendLine("Scoreboard:");
            this.ScoreBoard.Sort();
            for (var i = 0; i < this.ScoreBoard.Count; i++)
            {
                sb.AppendFormat("{0}. {1}", i, this.ScoreBoard[i]);
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
