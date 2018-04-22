using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using erlisp.IKeyWord;
using erlisp.IKeyWord.HelperKeyWord;

namespace erlisp
{
    internal class CodeGenerator
    {
        private static List<FoundKeyWord> tokenzedProgram = new List<FoundKeyWord>();
        public static int FunCounter;

        public static void AddNextToken(FoundKeyWord token)
        {
            tokenzedProgram.Add(token);
        }

        public static List<FoundKeyWord> GetTokenizedProgram() => tokenzedProgram;

        public static void Reprocess()
        {
            RemoveExtraComma();
            tokenzedProgram = tokenzedProgram.Where(x => x.GetKeyWordName() != "WhiteSpaces").ToList();
        }

        public static void RemoveExtraComma()
        {
            var first = new Comma();
            var second = new ClosingBracket();

            for (int i = 0; i < tokenzedProgram.Count - 2; i++)
            {
                if (tokenzedProgram[i].GetKeyWordName() == first.KeyWordName() &&
                    tokenzedProgram[i + 1].GetKeyWordName() == second.KeyWordName())
                {
                    tokenzedProgram.RemoveAt(i);
                }
            }
        }
        public static string GenerateCode()
        {
            var result = new StringBuilder();
            foreach (var token in tokenzedProgram)
            {
                result.Append(GenerateCode(token));
            }

            return result.ToString();
        }

        public static string GenerateCode(FoundKeyWord token)
        {
            var tokenType = token.GetKeyWordName();
            var tokenPattern = token.FoundPattern;
            switch (tokenType)
            {
                case "Comma":
                    return ",";
                case "IfStatment":
                    return "ifstm([";
                case "MatemticalOperator":
                    return GenerateCodeMatematicalOperator(token);
                case "EndOfCode":
                    return "." + Environment.NewLine;
                case "ClosingBracket":
                    return "])";
                case "Float":
                    return tokenPattern;
                case "Integer":
                    return tokenPattern;
                case "OpeningBracket":
                    FunCounter++;
                    return "fun" + FunCounter + "() -> ";
                case "Write":
                    return "write([";
                default:
                    return "Not Implemeted";
            }
        }

        public static string GenerateCodeMatematicalOperator(FoundKeyWord token)
        {
            var tokenPattern = token.FoundPattern;
            switch (tokenPattern)
            {
                case "+":
                    return "sum([";
                case "-":
                    return "sub([";
                case "*":
                    return "multiply([";
                case "/":
                    return "divide([";
                default:
                    return "Not Implemeted";
            }
        }
    }
}