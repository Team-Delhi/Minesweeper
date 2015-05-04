namespace MinesweeperProject
{
    using System;
    using System.Text;

    public class ScoreRecord : IComparable
    {
        /// <summary>
        /// Represents a score entry.
        /// </summary>
        /// <param name="playerName">Name of the player</param>
        /// <param name="score">Amount he scored</param>
        public ScoreRecord(string playerName, int score)
        {
            this.PlayerName = playerName;
            this.Score = score;
        }

        /// <summary>
        /// Name of score holder.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Amount of score.
        /// </summary>
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
