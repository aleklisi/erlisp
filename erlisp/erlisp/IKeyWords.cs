namespace erlisp
{
    interface IKeyWords
    {
        bool IsPartialMatch(string input);
        bool IsFullMatch(string input);
        string KeyWordName();
    }
}