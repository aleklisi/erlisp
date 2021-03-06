﻿namespace erlisp.IKeyWord
{
    class OpeningBracket : IKeyWords
    {
        public bool IsFullMatch(string input)
        {
            return input == "(";
        }

        public bool IsPartialMatch(string input)
        {
            return false;
        }

        public string KeyWordName()
        {
            return "OpeningBracket";
        }
    }

    class OpeningThread : IKeyWords
    {
        public bool IsFullMatch(string input)
        {
            return input == "[";
        }

        public bool IsPartialMatch(string input)
        {
            return false;
        }

        public string KeyWordName()
        {
            return "OpeningThread";
        }

    }
}