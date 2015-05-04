
namespace MinesweeperProject
{
    using System;
    using System.Text;

    internal class ScoreRecord : IComparable
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
            var score = obj as ScoreRecord;
            if (score != null)
            {
                return -1 * this.Score.CompareTo(score.Score);
            }

            throw new ArgumentException("Compare Object is not ScoreRecord!");
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} --> {1} cells \n", this.PlayerName, this.Score);
            return sb.ToString();
        }
    }
}
