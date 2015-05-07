namespace TestMinesweeperProject
{
    using System;
    using System.Linq;    
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MinesweeperProject;
    using MinesweeperProject.Exceptions;

    [TestClass]
    public class TestScoreRecord
    {
        ScoreRecord firstRecod = new ScoreRecord("Ivan", 15);
        ScoreRecord secondRecord = new ScoreRecord("Dragan", 20);
        

        [TestMethod]
        public void TestCompareTo()
        {
            int result = firstRecod.CompareTo(secondRecord);
            Assert.AreEqual(-1, result);
        }

       [TestMethod] 
        [ExpectedException(typeof(ArgumentException))]
        public void CompareToShouldThrowExceptionWhenArgumentIsNotValid()
        {
            MinesweeperCell cell = new MinesweeperCell();
            int result = firstRecod.CompareTo(cell);
        }

        [TestMethod]
       public void TestToString()
       {
           string record = firstRecod.ToString();
           string expected = "Ivan --> 15 cells\n";
           Assert.AreEqual(expected,record);
       }

    }
}
