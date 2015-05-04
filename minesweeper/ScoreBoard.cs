using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace MinesweeperProject
{
    class ScoreRecord:IComparable
    {
        public ScoreRecord(string playerName, int score)
        {
            this.PlayerName = playerName;
            this.Score = score;
        }

        public string PlayerName { get; set; }

        public int Score { get; set; }

        public int CompareTo(object obj)
        {
            if (!(obj is ScoreRecord))
            {
                throw
                    new ArgumentException("Compare Object is not ScoreRecord!");
            }

            return -1*this.Score.CompareTo(((ScoreRecord)obj).Score);
        }

        
    }

}
