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
        public static List<FoundKeyWord> _tokenzedProgram = new List<FoundKeyWord>();
        public static int FunCounter;
        public static int StackCounter;

        public static void AddNextToken(FoundKeyWord token)
        {
            _tokenzedProgram.Add(token);
        }

        public static List<FoundKeyWord> GetTokenizedProgram() => _tokenzedProgram;

        public static void Reprocess()
        {
            RemoveExtraComma();
            _tokenzedProgram = _tokenzedProgram.Where(x => x.GetKeyWordName() != "WhiteSpaces").ToList();
        }

        public static void RemoveExtraComma()
        {
            var first = new Comma();
            var second = new ClosingBracket();

            for (int i = 0; i < _tokenzedProgram.Count - 2; i++)
            {
                if (_tokenzedProgram[i].GetKeyWordName() == first.KeyWordName() &&
                    _tokenzedProgram[i + 1].GetKeyWordName() == second.KeyWordName())
                {
                    _tokenzedProgram.RemoveAt(i);
                }
            }
        }
        public static string GenerateCode()
        {
            var result = new StringBuilder();
            foreach (var token in _tokenzedProgram)
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
                case "Write":
                    return "write([";
                case "Comparator":
                    return GenerateCodeComperators(token);
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