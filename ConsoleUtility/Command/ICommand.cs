namespace ConsoleUtility
{
    public interface ICommand
    {
        void Execute(IWriter writer);
    }
}