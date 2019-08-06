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
        public void Parse_thread_ThreadCommand(params string[] args)
        {
            ConsoleWriter cw = new ConsoleWriter();

            Manager manager = new Manager(cw);
            manager.IdentifyCommand(args);

            manager._command.ForEach(x => x.ShouldBeOfType<ThreadSelectCommand>());
        }

        [Theory]
        [InlineData("-?")]
        [InlineData("--help")]
        [InlineData("--help", "-?")]
        public void Parse_Help_HelpCommand(params string[] args)
        {
            ConsoleWriter cw = new ConsoleWriter();

            Manager manager = new Manager(cw);
            manager.IdentifyCommand(args);

            manager._command.ForEach(x => x.ShouldBeOfType<HelpCommand>());
        }

        [Theory]
        [InlineData("-?", "-t")]
        [InlineData("--help", "--thread")]
        [InlineData("--help", "--thread", "-?", "-t")]
        public void Parse_HelpAndThread_HelpAndThreadCommands(params string[] args)
        {
            ConsoleWriter cw = new ConsoleWriter();

            Manager manager = new Manager(cw);
            manager.IdentifyCommand(args);
            foreach (var item in manager._command)
            {
                Assert.True(item is ICommand);
            }
        }

        [Theory]
        [InlineData("--thread", "2")]
        [InlineData("--thread", "2", "4")]
        public void Parse_ThreadWithArg_ThreadWithArg(params string[] args)
        {
            ConsoleWriter cw = new ConsoleWriter();

            Manager manager = new Manager(cw);
            manager.IdentifyCommand(args);
            foreach (var item in manager._command)
            {
                Assert.True(item is ThreadSelectCommand);
            }
        }

        [Theory]
        [InlineData("--help", "2")]
        public void Parse_HelpWithValue_ErrorCommand(params string[] args)
        {
            ConsoleWriter cw = new ConsoleWriter();

            Manager manager = new Manager(cw);
            manager.IdentifyCommand(args);
            foreach (var item in manager._command)
            {
                Assert.True(item is ErrorCommand);
            }
        }

        [Theory]
        [InlineData("--search", "искомая строка")]
        public void Parse_SearchWithString_SearchWithString(params string[] args)
        {
            ConsoleWriter cw = new ConsoleWriter();

            Manager manager = new Manager(cw);
            manager.IdentifyCommand(args);
            foreach (var item in manager._command)
            {
                Assert.True(item is SearchCommand);
            }
        }

        [Theory]
        [InlineData("--search", "искомая строка", "BadArg")]
        [InlineData("BadArg", "--search", "искомая строка")]
        public void Parse_SearchWithStringAndBadArg_ErrorCommand(params string[] args)
        {
            ConsoleWriter cw = new ConsoleWriter();

            Manager manager = new Manager(cw);
            manager.IdentifyCommand(args);
            foreach (var item in manager._command)
            {
                manager._command.ForEach(x => x.ShouldBeOfType<ErrorCommand>());
            }
        }

        [Theory]
        [InlineData("--search", "искомая строка", "-?")]
        [InlineData("-t", "4", "--search", "искомая строка", "-?")]
        public void Parse_FewArgs_Commands(params string[] args)
        {
            ConsoleWriter cw = new ConsoleWriter();

            Manager manager = new Manager(cw);
            manager.IdentifyCommand(args);
            foreach (var item in manager._command)
            {
                Assert.True(item is SearchCommand);
            }
        }
    }
}
