using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using erlisp.IKeyWord;
using erlisp.IKeyWord.HelperKeyWord;

namespace erlisp
{
    public class CodeGenerator
    {
        public static List<FoundKeyWord> TokenzedProgram = new List<FoundKeyWord>();
        public static int FunCounter;
        public static int StackCounter;

        public static void AddNextToken(FoundKeyWord token)
        {
            TokenzedProgram.Add(token);
        }

        public static List<FoundKeyWord> GetTokenizedProgram() => TokenzedProgram;

        public static void Reprocess()
        {
            RemoveExtraComma();
            TokenzedProgram = TokenzedProgram.Where(x => x.GetKeyWordName() != "WhiteSpaces").ToList();
        }

        public static void RemoveExtraComma()
        {
            var first = new Comma();
            var secondB = new ClosingBracket();
            var secondT = new ClosingThread();

            for (var i = 0; i < TokenzedProgram.Count - 2; i++)
            {
                if (TokenzedProgram[i].GetKeyWordName() == first.KeyWordName() &&
                    (TokenzedProgram[i + 1].GetKeyWordName() == secondB.KeyWordName() ||
                     TokenzedProgram[i + 1].GetKeyWordName() == secondT.KeyWordName()))
                {
                    TokenzedProgram.RemoveAt(i);
                }
            }
        }

        public static string GenerateMain()
        {
            var main = new StringBuilder();
            main.Append("main() -> ");
            int i;
            for (i = 1; i <= FunCounter-1; i++)
            {
                main.Append("fun" + i + "(),\n");
            }
            main.Append("fun" + i + "().");
            return main.ToString();
        }
        public static string GenerateCode()
        {
            var result = new StringBuilder();
            foreach (var token in TokenzedProgram)
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
                case "Strings":
                    return tokenPattern;
                case "Comma":
                    return ",";
                case "IfStatment":
                    return "ifstm([";
                case "MatemticalOperator":
                    return GenerateCodeMatematicalOperator(token);
                case "EndOfCode":
                    return "." + Environment.NewLine;
                case "ClosingBracket":
                    StackCounter--;
                    return "])";
                case "Float":
                    return tokenPattern;
                case "Integer":
                    return tokenPattern;
                case "OpeningBracket":
                {
                    if (StackCounter++ == 0)
                        {
                            FunCounter++;
                            return "fun" + FunCounter + "() -> ";
                        }

                    return "";
                    }
                case "OpeningThread":
                    if (StackCounter++ == 0)
                    {
                        FunCounter++;
                        return "fun" + FunCounter + "() -> spawn(";
                    }
                    return "";

                case "ClosingThread":
                    StackCounter--;
                    return "])";
                case "Write":
                    return "write([";
                case "Comparator":
                    return GenerateCodeComperators(token);
                case "InLineErlang":
                    return token.FoundPattern.Remove(token.FoundPattern.Length-1,1).Remove(0,1);
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
        public static string GenerateCodeComperators(FoundKeyWord token)
        {
            var tokenPattern = token.FoundPattern;
            var result = new StringBuilder();
            result.Append("cmp(");
            string x;
            switch (tokenPattern)
            {
                case ">":
                { x = "gt,";}
                    break;
                case "<":
                    x = "sm,";
                    break;
                case ">=":
                    x = "gteq,";
                    break;
                case "<=":
                    x = "smeq,";
                    break;
                case "=":
                    x = "eq,";
                    break;
                case "/=":
                    x = "noteq,";
                    break;
                default:
                    return "Not Implemeted";
            }

            result.Append(x);
            result.Append("[");
            return result.ToString();
        }
    }
}