using System.Linq;

namespace erlisp.IKeyWord
{
    class Variable : IKeyWords
    {
        public bool IsPartialMatch(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            if (!char.IsUpper(input[0]))
            {
                return false;
            }

            return input.Skip(1).All(char.IsLetter);
        }

        public bool IsFullMatch(string input)
        {
            return IsPartialMatch(input);
        }

        public string KeyWordName()
        {
            return "Variable";
        }
    }
}