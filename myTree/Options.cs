using System.Collections.Generic;
using System;
using myTree;

namespace myTree
{
    public class Options
    {
        public bool WasError { get; }
        public bool NeedHumanReadable { get; }
        public bool NeedSize { get; }
        public bool NeedHelp { get; }
        public int Depth { get; }

        public struct Sorting
        {
            public bool OrderBySize { get; }
            public bool OrderByDateOfTransorm { get; }
            public bool OrderByDateOfCreation { get; }
            public bool Reverse { get; }

            public Sorting(bool orderBySize, bool orderByDateOfTransorm, bool orderByDateOfCreation, bool reverse)
            {
                OrderBySize = orderBySize;
                OrderByDateOfTransorm = orderByDateOfTransorm;
                OrderByDateOfCreation = orderByDateOfCreation;
                Reverse = reverse;
            }
        }

        public Sorting sorting;
        public Options(bool wasError, bool needSize, bool needHumanReadable, bool needHelp,
        int depth, bool orderBySize, bool orderByDateOfTransorm,
        bool orderByDateOfCreation, bool reverse)
        {
            WasError = wasError;
            NeedSize = needSize;
            NeedHumanReadable = needHumanReadable;
            NeedHelp = needHelp;
            Depth = depth;

            sorting = new Sorting(orderBySize, orderByDateOfTransorm, orderByDateOfCreation, reverse);
        }
    }
}