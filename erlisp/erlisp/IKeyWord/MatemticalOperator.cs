namespace erlisp.IKeyWord
{
    class MatemticalOperator : IKeyWords
    {
        public bool IsPartialMatch(string input)
        {
            return false;
        }

        public bool IsFullMatch(string input)
        {
            switch (input)
            {
                case "+":
                    return true;
                case "-":
                    return true;
                case "*":
                    return true;
                case "/":
                    return true;
            }

            return false;
        }

        public string KeyWordName()
        {
            return "MatemticalOperator";
        }
    }
}