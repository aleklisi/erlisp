using System;
using System.Linq;

namespace erlisp.IKeyWord
{
    class WhiteSpaces : IKeyWords
    {
        public bool IsPartialMatch(string input)
        {
            return input.All(Char.IsWhiteSpace);
        }

        public bool IsFullMatch(string input)
        {
            return input.All(Char.IsWhiteSpace);
        }

        public string KeyWordName()
        {
            return "WhiteSpaces";
        }
    }
}