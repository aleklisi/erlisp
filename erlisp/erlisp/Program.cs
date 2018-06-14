using System;
using System.Collections.Generic;

namespace erlisp
{
    class Program
    {

        public static void Main()
        {
            Console.WriteLine("Skaner Started!!!");
            var textFromFile = "(+1 2 3)";
            var testSkaner = new LispScaner(textFromFile);

            try
            {
                var tokenizeInput = testSkaner.TokenizeInput();

                //PrintResults(tokenizeInput);
                Console.WriteLine("Skaner Finished!!!");

                Console.WriteLine("Parser Started!!!");
                var parsed = Parser.Parse(tokenizeInput);
                Console.WriteLine("Parser result is: " + parsed);
                Console.WriteLine("Parser  Finished!!!");
                // PrintResults(CodeGenerator.GetTokenizedProgram());

                Console.WriteLine("Generator Started!!!");
                Console.WriteLine(CodeGenerator.GenerateCode());
                Console.WriteLine(CodeGenerator.GenerateMain());
                Console.WriteLine("Generator Finished!!!");

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
