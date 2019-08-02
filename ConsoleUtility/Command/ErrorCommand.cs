using ConsoleUtility;
namespace ConsoleUtility
{
    public class ErrorCommand : ICommand
    {
        public void Execute(IWriter writer)
        {
            writer.Write("ERROR");
        }
    }
}