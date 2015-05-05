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
        [ExpectedException(typeof(ArgumentOutOfRangeException))]

        public void MinesweeperGridShouldThrowExceptionWhenTheCountOfMinesIsInvalid()
        {
            MinesweeperGrid thirdGrid = new MinesweeperGrid(2, 2, 4);           
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]

        public void MinesweeperGridShouldThrowExceptionWhenTheCountOfMinesIsZero()
        {
            MinesweeperGrid secondGrid = new MinesweeperGrid(2, 2, 0);
        }

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
        public void RevealSellShouldThroughExceptionWhenRowIsInvalid()
        {
            grid.RestartBoard();
            grid.RevealCell(10, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCellException))]
        public void RevealSellShouldThroughExceptionWhenColumnIsInvalid()
        {
            grid.RestartBoard();
            grid.RevealCell(0, 20);
        }

        [TestMethod]

        public void TestGetNeibourMinesCountWithOneMine()
        {
            MinesweeperGrid secondGrid = new MinesweeperGrid(2, 2, 1);
            secondGrid.RestartBoard();
            int numberOfNeighbourMines = secondGrid.GetNeighbourMinesCount(0, 0);
            Assert.AreEqual(1, numberOfNeighbourMines);
        }

        [TestMethod]

        public void TestGetNeibourMinesCount()
        {
            MinesweeperGrid secondGrid = new MinesweeperGrid(2, 2, 3);
            secondGrid.RestartBoard();
            int numberOfNeighbourMines = secondGrid.GetNeighbourMinesCount(0, 0);
            bool isTwoOrThree = (numberOfNeighbourMines == 2 || numberOfNeighbourMines == 3);
            Assert.IsTrue(isTwoOrThree);
        }

        //[TestMethod]

        //public void TestMarkUnrevealedMines()
        //{
        //    MinesweeperGrid secondGrid = new MinesweeperGrid(2, 2, 3);
        //    secondGrid.RestartBoard();
        //    secondGrid.RevealMines();
        //    int count = secondGrid.ToString().Count(f => f == '-');
        //    Assert.AreEqual(1, count);
        //}
    }
}
