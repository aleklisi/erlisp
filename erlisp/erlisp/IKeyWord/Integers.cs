using System.Linq;

namespace erlisp.IKeyWord
{
    class Integers : IKeyWords
    {

        public bool IsFullMatch(string input)
        {
            return input.All(char.IsDigit);
        }

        public bool IsPartialMatch(string input)
        {
            return false;
        }

        public string KeyWordName()
        {
            return "Integer";
        }

    }
}