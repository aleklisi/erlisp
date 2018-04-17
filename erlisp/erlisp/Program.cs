using System;
using System.Collections.Generic;
using System.IO;

namespace erlisp
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Start");
            var textFromFile = "\"\"";
            var testSkaner = new LispScaner(textFromFile);

            try
            {
                var result = testSkaner.TokenizeInput();
                PrintResults(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();

            Console.ReadKey();
        }
        static void PrintResults(IEnumerable<FoundKeyWord> keyWords)
        {
            foreach (var keyWord in keyWords)
            {
                Console.WriteLine("Pattern is: " + keyWord.FoundPattern + " tokanized as: " + keyWord.KeyWordType.KeyWordName());
            }
        }
    }
}
