using System;
using System.IO;

namespace erlisp
{
    class Program
    {

        public static void Main(string []args)
        {
            string input;
            try
            {
                
                if (args.Length == 0)
                {
                    Console.WriteLine("Please provide filename as program argument!!!");
                    Console.ReadKey();
                    return;
                }
                input = File.ReadAllText(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening file:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType().ToString());
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Skaner Started!!!");
            var testSkaner = new LispScaner(input);

            try
            {
                var tokenizeInput = testSkaner.TokenizeInput();

                Console.WriteLine("Skaner Finished!!!");

                Console.WriteLine("Parser Started!!!");
                var parsed = Parser.Parse(tokenizeInput);
                Console.WriteLine("Parser result is: " + parsed);
                Console.WriteLine("Parser  Finished!!!");

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
    }
}
