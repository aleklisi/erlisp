using System.Collections.Generic;

namespace erlisp.IKeyWord
{
    class Function : IKeyWords
    {
        static readonly List<string> MatchingRegex = new List<string>();

        public Function()
        {
            MatchingRegex.Add("defun");
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
            return "Function";
        }

        public int NuberOfArguments()
        {
            return 4;
        }
    }
}