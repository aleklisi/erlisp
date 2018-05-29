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
        public static bool InFun;

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
            var second = new ClosingBracket();

            for (int i = 0; i < TokenzedProgram.Count - 3; i++)
            {
                if (TokenzedProgram[i].GetKeyWordName() == first.KeyWordName() &&
                    TokenzedProgram[i + 1].GetKeyWordName() == second.KeyWordName())
                {
                    TokenzedProgram.RemoveAt(i);
                }
            }
        }
        public static string GenerateCode()
        {
            var result = new StringBuilder();
            for (var i = 0; i < TokenzedProgram.Count-1; i++)
            {
                if (TokenzedProgram[i].KeyWordType.KeyWordName() != "OpeningBracket" ||
                    TokenzedProgram[i + 1].KeyWordType.KeyWordName() != "Function")
                {
                    result.Append(GenerateCode(TokenzedProgram[i]));
                }
                else
                {
                    InFun = true;
                    while (TokenzedProgram[i].KeyWordType.KeyWordName() != "Variable") i++;
                    result.Append(TokenzedProgram[i].FoundPattern.ToLower() + "(");
                    i+=2;
                    while (TokenzedProgram[i].KeyWordType.KeyWordName() == "Variable")
                    {
                        result.Append(TokenzedProgram[i].FoundPattern + ",");
                        i += 1;
                    }
                    result.Length--;
                    result.Append(")->");
                    i++;
                    while (TokenzedProgram[i].KeyWordType.KeyWordName() != "ClosingBracket")
                    {
                        result.Append(GenerateCode(TokenzedProgram[i]));
                        i += 1;
                    }
                    if(result[result.Length-1] == ',') result.Length--;
                    //while (StackCounter > 1)
                    //{
                    //    result.Append("])");
                    //    StackCounter--;
                    //}
                    //result.Append(".");
                    InFun = false;
                }
            }
            while (StackCounter > 1)
            {
                result.Append("])");
                StackCounter--;
            }
            result.Append(".");
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
                    StackCounter++;
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
                    if (!InFun)
                    {
                        if (StackCounter++ == 0)
                        {
                            FunCounter++;
                            return "fun" + FunCounter + "() -> ";
                        }
                        }
                    else
                    {
                        StackCounter++;
                    }

                    return "";
                }
                case "Write":
                    StackCounter++;
                    return "write([";
                case "Comparator":
                    return GenerateCodeComperators(token);
                case "Variable":
                    return tokenPattern +",";
                case "WhiteSpaces":
                    return "";
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