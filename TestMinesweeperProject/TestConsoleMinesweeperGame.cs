namespace TestMinesweeperProject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MinesweeperProject;
    using MinesweeperProject.Exceptions;
    

    [TestClass]
    public class TestConsoleMinesweeperGame
    {    
        [TestMethod]
        public void TestGameStart()
        {
            ConsoleMinesweeperGame game = ConsoleMinesweeperGame.Instance(2, 2, 1);
            game.Grid.RestartBoard();          
            string grid = game.Grid.ToString();
            int countOfCells = grid.Count(star => star == '?');
            Assert.AreEqual(4, countOfCells);
        }

        [TestMethod]
        public void TestGridInitializationInTheGame()
        {
            ConsoleMinesweeperGame game = ConsoleMinesweeperGame.Instance(2, 2, 1);
            game.Grid.RestartBoard();
            game.Grid.RevealMines();
            string grid = game.Grid.ToString();            
            int countOfMines = grid.Count(star => star=='*');
            Assert.AreEqual(1, countOfMines);
        }

        [TestMethod]

        public void TestExit()
        {
            ConsoleMinesweeperGame game = ConsoleMinesweeperGame.Instance(2, 2, 1);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                game.Exit();

                string expected = string.Format("Good bye!{0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString());
            }
        }

        [TestMethod]

        public void TestPrintScoreBoard()
        {
            ConsoleMinesweeperGame game = ConsoleMinesweeperGame.Instance(10, 10, 5);
            ScoreRecord firstRec = new ScoreRecord("Petkan", 15);
            ScoreRecord secondRec = new ScoreRecord("Penka", 18);
            ScoreRecord thirdRec = new ScoreRecord("Pesho", 5);

            game.ScoreBoard.Add(firstRec);           

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                game.PrintScoreBoard();

                string expected = "Scoreboard:\n1. Petkan --> 15 cells\n";
                Assert.AreEqual<string>(expected, sw.ToString());
            }
        }

        [TestMethod]
        public void TestNextCommand()
        {
            using (StringWriter sw = new StringWriter())
            {
                ConsoleMinesweeperGame game = ConsoleMinesweeperGame.Instance(10, 10, 5);

                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("exit{0}",
            Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    game.NextCommand();

                    string expected = string.Format(
                        "Enter command or row and column: Good bye!{0}",
                        Environment.NewLine);
                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }
        }

        //[TestMethod]
        //public void TestStart()
        //{
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        ConsoleMinesweeperGame game = ConsoleMinesweeperGame.Instance(10, 10, 5);
        //        game.Start();
        //        string startMessage = "Welcome to the game “Minesweeper”. Try to reveal all cells without mines. " +
        //                                    "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' " +
        //                                    "to quit the game.";
        //        string grid = game.Grid.ToString();

        //        Console.SetOut(sw);

        //        using (StringReader sr = new StringReader(string.Format("exit{0}",
        //    Environment.NewLine)))
        //        {
        //            Console.SetIn(sr);

        //            game.NextCommand();

        //            string expected = startMessage + Environment.NewLine + grid +Environment.NewLine+
        //                "Enter command or row and column: Good bye!" +
        //                Environment.NewLine;

        //            Assert.AreEqual<string>(expected, sw.ToString());
        //        }
        //    }
        //}

        //[TestMethod]
        //public void TestStart()
        //{
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        ConsoleMinesweeperGame game = ConsoleMinesweeperGame.Instance(10, 10, 5);
        //        game.Grid.RestartBoard();
        //        string startMessage = "Welcome to the game “Minesweeper”. Try to reveal all cells without mines. " +
        //                                    "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' " +
        //                                    "to quit the game.";
        //        string grid = "" + game.Grid.ToString();
        //        game.Grid.RevealMines();
        //        string revealed = game.Grid.ToString();

        //        Console.SetOut(sw);

        //        using (StringReader sr = new StringReader(string.Format("nakov{0},exit{0}",
        //    Environment.NewLine)))
        //        {
        //            Console.SetIn(sr);

        //            game.NextCommand();

        //            string expected = startMessage + grid +
        //                "Enter command or row and column: " + revealed + Environment.NewLine +
        //                "Enter command or row and column: Good bye!" + Environment.NewLine
        //                ;

        //            Assert.AreEqual<string>(expected, sw.ToString());
        //        }
        //    }
        //}
    }
}
