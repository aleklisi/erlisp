using System;
using System.Collections.Generic;

namespace erlisp.IKeyWord
{
    class IfStatment : IFunction
    {
        static readonly List<string> MatchingRegex = new List<string>();

        public IfStatment()
        {
            MatchingRegex.Add("if");
        }

        public bool IsFullMatch(string input)
        {
            return MatchingService.IsFullMatch(MatchingRegex, input);
        }

        public bool IsPartialMatch(string input)
        {
            return MatchingService.IsPartialMatch(MatchingRegex, input);
        }

        public string KeyWordName()
        {
            return "IfStatment";
        }

        public int NuberOfArguments()
        {
            return 3;
        }
    }
}