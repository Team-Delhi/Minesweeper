namespace MinesweeperProject
{
    using System;

    class ScoreRecord : IComparable
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
            var score = obj as ScoreRecord;
            if (score != null)
            {
                return -1 * this.Score.CompareTo(score.Score);
            }

            throw new ArgumentException("Compare Object is not ScoreRecord!");
        }
    }
}
