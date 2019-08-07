using ConsoleUtility;
namespace ConsoleUtility
{
    public class LineFoundSearchCommand : ICommand
    {
        public int Value { get; set; }
        public LineFoundSearchCommand()
        {
            Value = -1;
        }
    }
}