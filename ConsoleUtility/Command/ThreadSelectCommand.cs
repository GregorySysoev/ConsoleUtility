using ConsoleUtility;
namespace ConsoleUtility
{
    [CommandPrefix(new string[] { "-t", "--thread" })]

    public class ThreadSelectCommand : ICommand
    {
        public int Value { get; set; }

        public ThreadSelectCommand()
        {
            Value = 1;
        }
    }
}