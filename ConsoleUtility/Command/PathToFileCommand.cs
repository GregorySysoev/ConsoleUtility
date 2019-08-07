using ConsoleUtility;
namespace ConsoleUtility
{
    public class PathToFileCommand : ICommand
    {
        public string Value { get; set; }

        public PathToFileCommand()
        {
            Value = "";
        }
    }
};
