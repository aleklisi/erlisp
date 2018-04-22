using System;
using System.Collections.Generic;

namespace erlisp
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Skaner Started!!!");
            var textFromFile = "(if () 1 (+ 2 3))";
            var testSkaner = new LispScaner(textFromFile);

            try
            {
                var result = testSkaner.TokenizeInput();
                //PrintResults(result);
                Console.WriteLine("Skaner Finished!!!");
                Console.WriteLine("Parser Started!!!");
                var parsed = Parser.Parse(result);
                Console.WriteLine("Parser result is: " + parsed);
                Console.WriteLine("Parser  Finished!!!");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
