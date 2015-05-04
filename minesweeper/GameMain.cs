namespace MinesweeperProject
{
    public class GameMain
    {
        public static void Main()
        {
            var game = ConsoleMinesweeperGame.Instance(5, 10, 5);
            game.Start();
        }
    }
}
