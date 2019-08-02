using ConsoleUtility;
namespace ConsoleUtility
{
    public class PrintErrorCommand : ICommand
    {
        public void Execute(IWriter writer)
        {
            writer.Write("ERROR");
        }
    }
}