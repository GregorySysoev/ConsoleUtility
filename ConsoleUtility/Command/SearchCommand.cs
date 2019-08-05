using ConsoleUtility;

namespace ConsoleUtility
{
    [CommandPrefix(new string[] { "-s", "--search" })]
    public class SearchCommand : ICommand
    {
        public string Value { get; set; }
        public SearchCommand()
        {

        }
    }
}