using System.Collections.Generic;
using System.IO;
using System;

namespace myTree
{
    public class Printer
    {
        private static IWriter _writer;
        public static void Print(ref Options options, IWriter writer, string path)
        {
            _writer = writer;

            if (options.WasError)
            {
                _writer.Write("Bad args");
                _writer.Write("use --help");
                return;
            }

            if (options.NeedHelp)
            {
                PrintHelp();
                return;
            }

            List<int> pipes = new List<int>();
            PrintRecursively(options.Depth, 0, path, ref pipes, ref options);
        }

        public static void PrintRecursively(int depth, int indent, string path, ref List<int> pipes, ref Options options)
        {
            if (depth == 0)
            {
                return;
            }

            var dir = new DirectoryInfo(path);
            var info = dir.GetFileSystemInfos();
            pipes.Add(indent);

            SortArray(ref info, options);

            for (int i = 0, localDepth = --depth; i < info.Length; i++)
            {
                string prefix = "";
                string suffix = "";
                if (i == info.Length - 1)
                {
                    pipes.Remove(indent);
                    prefix = PrintIndent(indent, ref pipes);
                    prefix += ("└──");

                }
                else
                {
                    prefix += PrintIndent(indent, ref pipes);
                    prefix += ("├──");
                }

                if (info[i] is DirectoryInfo dInfo)
                {
                    if (options.sorting.OrderByDateOfCreation)
                    {
                        suffix = (" " + dInfo.CreationTime.ToString());
                    }
                    else if (options.sorting.OrderByDateOfTransorm)
                    {
                        suffix = (" " + dInfo.LastWriteTime.ToString());
                    }
                    _writer.Write(prefix, dInfo, suffix);
                    PrintRecursively(localDepth, indent + 4, dInfo.FullName, ref pipes, ref options);
                }
                else if (info[i] is FileInfo fInfo)
                {
                    if (options.NeedHumanReadable | options.NeedSize)
                    {
                        suffix = (" " + PrintSize(fInfo.Length, options.NeedHumanReadable));
                    }

                    if (options.sorting.OrderByDateOfCreation)
                    {
                        suffix = (" " + fInfo.CreationTime.ToString());
                    }
                    else if (options.sorting.OrderByDateOfTransorm)
                    {
                        suffix = (" " + fInfo.LastWriteTime.ToString());
                    }

                    _writer.Write(prefix, fInfo, suffix);
                }
            }
            pipes.Remove(indent);
        }
        public static void PrintHelp()
        {
            _writer.Write("");
            _writer.Write("List of available commands:");
            _writer.Write("");
            _writer.Write("-d --depth [num]  nesting level");
            _writer.Write("-s --size  show size of files");
            _writer.Write("-h --human-readable  show size of files in human-readable view {Bytes, KB, ...}");
            _writer.Write("-r reverse order of elements");
            _writer.Write("-o --order-by [flag] order of elements in tree. Default - by alphabet");
            _writer.Write("");
            _writer.Write("Available flags:");
            _writer.Write("s - order by size");
            _writer.Write("t - order by time of last change");
            _writer.Write("c - order by time of creation");
            _writer.Write("");
            _writer.Write("");
            _writer.Write("Attation");
            _writer.Write("If you are using 'dotnet myTree.dll' without args - you will see a whole tree");
            _writer.Write("If you are using 'dotnet myTree.dll' without '--order-by [s, t, c]' - default tree will be sorted by alphabet");
            _writer.Write("");
            _writer.Write("--help show help");
            _writer.Write("");
            _writer.Write("Example of using:");
            _writer.Write("dotnet myTree.dll -d 5 -h -o a");
            _writer.Write("");
        }
        public static string PrintIndent(int indent, ref List<int> pipes)
        {
            string result = "";
            for (int i = 0; i < indent; i++)
            {
                if (i % 4 == 0)
                {
                    if (pipes.Contains(i))
                    {
                        result += ("│");
                    }
                }
                else
                {
                    result += (" ");
                }
            }
            return result;
        }
        public static string PrintSize(long num, bool humanRead)
        {
            if (num == 0)
            {
                return ("(empty)");
            }

            if (!humanRead)
            {
                return string.Format($"({num} Bytes)");
            }
            else
            {
                return GetHumanReadableSizeView(num);
            }
        }
        public static string GetHumanReadableSizeView(long num)
        {
            string[] suffixes =
    { "Bytes", "KB", "MB", "GB", "TB", "PB" };

            int counter = 0;
            decimal number = (decimal)num;
            while ((counter < 5) && (Math.Round(number / 1024) >= 1))
            {
                number /= 1024;
                counter++;
            }

            if (number - decimal.Truncate(number) == 0)
            {
                return string.Format("({0:} {1})", number, suffixes[counter]);
            }
            else
            {
                return string.Format("({0:n1} {1})", number, suffixes[counter]);
            }
        }
        public static void SortArray(ref FileSystemInfo[] array, Options options)
        {
            if (options.sorting.OrderBySize)
            {
                SizeComparer sizeComparer = new SizeComparer();
                Array.Sort(array, sizeComparer);
            }
            else if (options.sorting.OrderByDateOfTransorm)
            {
                ChangeTimeComparer changeTimeComparer = new ChangeTimeComparer();
                Array.Sort(array, changeTimeComparer);
            }
            else if (options.sorting.OrderByDateOfCreation)
            {
                CreateTimeComparer createTimeComparer = new CreateTimeComparer();
                Array.Sort(array, createTimeComparer);
            }
            else
            {
                NameComparer nameComparer = new NameComparer();
                Array.Sort(array, nameComparer);
            }

            if (options.sorting.Reverse)
            {
                Array.Reverse(array);
            }
        }
    }
}