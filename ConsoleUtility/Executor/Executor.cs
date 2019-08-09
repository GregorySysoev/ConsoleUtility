using System;
using ConsoleUtility;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ConsoleUtility
{
    public class Executor
    {
        private List<ICommand> _commands;
        private IPrinter _printer;
        public Executor(List<ICommand> commands, IPrinter printer)
        {
            _commands = commands;
            _printer = printer;
        }
        public void Execute()
        {
            int countOfThreads = 1;
            string stringToSearch = "";
            string pathToFind = System.Environment.CurrentDirectory;

            bool help = false;
            bool error = false;

            for (int i = 0; i < _commands.Count && !error && !help; i++)
            {
                switch (_commands[i])
                {
                    case ErrorCommand errorCommand:
                        error = true;
                        break;
                    case HelpCommand helpCommand:
                        help = true;
                        break;
                    case ThreadSelectCommand threadCount:
                        _commands.Remove(threadCount);
                        countOfThreads = threadCount.Value;
                        i--;
                        break;
                    case SearchCommand search:
                        stringToSearch = search.Value;
                        _commands.Remove(search);
                        i--;
                        break;
                    default:
                        continue;
                }
            }

            if (error)
            {
                _printer.Print("Произошла ошибка: некорректно введены аргументы");
                _printer.Print("Введите dotnet ConsoleUtility.dll --help чтобы получить помощь");
                return;
            }

            if (help)
            {
                _printer.Print("");
                _printer.Print("Список доступных команд:");
                _printer.Print("-t --thread [число] - установить кол-во потоков");
                _printer.Print("-s --search [\"строка\"] - установить строку для поиска. Обязательная комадна");
                _printer.Print("-? --help - помощь");
                _printer.Print("");
                _printer.Print("Пример использования");
                _printer.Print("dotnet ConsoleUtility.dll -t 2 --search \"public class Program\"");
                _printer.Print("");
                return;
            }

            if (stringToSearch == "")
            {
                _printer.Print("Искомая строка не указана, используйте -s \"искомая строка\"");
                return;
            }
            Finder finder = new Finder(stringToSearch, pathToFind, _commands, countOfThreads, _printer);
            finder.Find();
        }
    }
}