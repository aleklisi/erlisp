using System.Collections.Generic;

namespace erlisp.IKeyWord
{
    class Write : IFunction
    {
        static readonly List<string> MatchingRegex = new List<string>();

        public Write()
        {
            MatchingRegex.Add("write");
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
            return "Write";
        }

        public int NuberOfArguments()
        {
            return 1;
        }

    }
}