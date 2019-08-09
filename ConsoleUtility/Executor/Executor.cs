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
                        break;
                    case SearchCommand search:
                        _commands.Remove(search);
                        stringToSearch = search.Value;
                        break;
                    default:
                        continue;
                }
            }


            if (error)
            {
                Console.WriteLine("Произошла ошибка: некорректно введены аргументы");
                return;
            }
            if (stringToSearch == "")
            {
                Console.WriteLine("Искомая строка не указана, используйте -s \"искомая строка\"");
                return;
            }
            if (help)
            {
                Console.WriteLine("Список доступных команд:");
                Console.WriteLine("-t --thread [число] - установить кол-во потоков");
                Console.WriteLine("-s --search [\"строка\"] - установить строку для поиска. Обязательная комадна");
                Console.WriteLine("-? --help - помощь");
                Console.WriteLine("");
                Console.WriteLine("Пример использования");
                Console.WriteLine("dotnet ConsoleUtility.dll -t 2 --search \"public class Program\"");
                return;
            }

            Finder finder = new Finder(stringToSearch, pathToFind, _commands, countOfThreads, _printer);
            finder.Find();
        }
    }
}