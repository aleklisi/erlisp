using System;
using System.Collections.Generic;

namespace erlisp
{
    class Program
    {

        public static void Main()
        {
            Console.WriteLine("Skaner Started!!!");
            var textFromFile = "(defun Name (Arga Argb Argc) (+ Arga Argb Argc))";
            var testSkaner = new LispScaner(textFromFile);

            try
            {
                var tokenizeInput = testSkaner.TokenizeInput();

                Console.WriteLine("Skaner Finished!!!");
                Console.WriteLine("Parser Started!!!");
                var parsed = Parser.Parse(tokenizeInput);
                if (parsed)
                {
                    Console.WriteLine("Parser result is: " + true);
                    Console.WriteLine("Parser  Finished!!!");
                    Console.WriteLine(CodeGenerator.GenerateCode());
                }
                else
                {
                    Console.WriteLine("Parser result is: " + false);
                    Console.WriteLine("Parser  Finished!!!");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();

        }

        private static void PrintResults(IEnumerable<FoundKeyWord> keyWords)
        {
            foreach (var keyWord in keyWords)
            {
                Console.WriteLine("Pattern is: " + keyWord.FoundPattern + " tokanized as: " + keyWord.KeyWordType.KeyWordName());
            }
        }
    }
}
