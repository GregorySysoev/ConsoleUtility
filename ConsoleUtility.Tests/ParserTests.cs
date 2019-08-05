using System;
using Xunit;
using Shouldly;

namespace ConsoleUtility.Tests
{
    public class ParserTests
    {
        [Theory]
        [InlineData("-t")]
        [InlineData("--thread")]
        [InlineData("--help")]
        public void Test1(params string[] args)
        {
            ConsoleWriter cw = new ConsoleWriter();

            Manager manager = new Manager(cw);
            manager.IdentifyCommand(args);

            manager._command.ShouldBeOfType<ICommand>();
        }
    }
}
