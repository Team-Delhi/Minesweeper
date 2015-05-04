namespace MinesweeperProject.Interfaces
{
    public interface IMinesweeperCell
    {
        char VisibleValue { get; }

        char Value { get; }
        
        bool Revealed { get; }
    }
}
