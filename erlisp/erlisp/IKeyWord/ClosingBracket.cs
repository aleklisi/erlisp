namespace erlisp.IKeyWord
{
    class ClosingBracket : IKeyWords
    {
        public bool IsFullMatch(string input)
        {
            return input == ")";
        }

        public bool IsPartialMatch(string input)
        {
            return false;
        }

        public string KeyWordName()
        {
            return "ClosingBracket";
        }
    }
}