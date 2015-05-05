using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinesweeperProject;
using MinesweeperProject.Exceptions;

namespace TestMinesweeperProject
{
    [TestClass]
    public class UnitTest1
    {
        MinesweeperGrid grid = new MinesweeperGrid(5, 10, 15);  

        [TestMethod]

        public void TestNumberOfMinesAfterRevealing()
        {
            grid.RestartBoard();
            grid.RevealMines();
            int count = grid.ToString().Count(f => f == '*');
            Assert.AreEqual(15, count);
        }

        [TestMethod]
        public void TestNumberOfUnrevealedSellsBeforeRevealing()
        {
            grid.RestartBoard();
            int count = grid.ToString().Count(f => f == '?');
            Assert.AreEqual(50, count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCellException))]
        public void RevealSellShouldThroughExceptionWhenSellIsInvalid()
        {
            grid.RestartBoard();
            grid.RevealCell(10, 5);
        }

         [TestMethod]
        public void TestRevealSell()
        {
            MinesweeperGrid secondGrid = new MinesweeperGrid(2, 2, 4);
            secondGrid.RestartBoard();
            char symbol = secondGrid.RevealCell(1, 1);
            Assert.AreEqual('*', symbol);             
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]

         public void TestRevealSellWhenNoMines()
         {
             MinesweeperGrid secondGrid = new MinesweeperGrid(2, 2, 0);
             secondGrid.RestartBoard();
             char symbol = secondGrid.RevealCell(1, 1);             
         }


        

        [TestMethod]

        public void TestGetNeibourMinesCount()
        {
            MinesweeperGrid secondGrid = new MinesweeperGrid(2, 2, 1);
            secondGrid.RestartBoard();
            int numberOfNeighbourMines = secondGrid.GetNeighbourMinesCount(0, 0);
            Assert.AreEqual(1, numberOfNeighbourMines);
        }

        [TestMethod]
        
        public void GetNeibourMinesCountShouldReturn()
        {
            MinesweeperGrid thirdGrid = new MinesweeperGrid(2, 2, 4);
            thirdGrid.RestartBoard();
            int numberOfNeighbourMines = thirdGrid.GetNeighbourMinesCount(0, 0);
            Assert.AreEqual(4, numberOfNeighbourMines);
        }


        
    }
}
