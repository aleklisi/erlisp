using System.Linq;

namespace erlisp.IKeyWord
{
    class Floats : IKeyWords
    {

        public bool IsFullMatch(string input)
        {
            var splitInput = input.Split('.');

            if (splitInput.Length != 2)
                return false;

            var result = splitInput[0].All(char.IsDigit) && splitInput[0].Length > 0 &&
                         splitInput[1].All(char.IsDigit) && splitInput[1].Length > 0;

            return result;
        }

        public bool IsPartialMatch(string input)
        {
            var splitInput = input.Split('.');

            return splitInput.Length == 1 && splitInput[0].All(char.IsDigit) ||
                   splitInput.Length == 2 && splitInput[0].All(char.IsDigit) && splitInput[1].All(char.IsDigit);

        }

        public string KeyWordName()
        {
            return "Float";
        }
    }
}