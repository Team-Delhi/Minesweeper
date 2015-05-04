namespace MinesweeperProject
{
    public class GameMain
    {
        static void Main()
        {
            var game = ConsoleMinesweeperGame.Instance(5,10,15);

            game.Start();
        }
    }
}
