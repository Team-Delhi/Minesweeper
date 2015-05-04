namespace MinesweeperProject
{
    using System;

    class ScoreRecord:IComparable
    {
        public ScoreRecord(string playerName, int score)
        {
            this.PlayerName = playerName;
            this.Score = score;
        }

        public string PlayerName { get; set; }

        public int Score { get; set; }

        public int CompareTo(Object obj)
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
