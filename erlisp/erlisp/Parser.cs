using System.Collections.Generic;
using System.Linq;
using erlisp.IKeyWord;
using erlisp.IKeyWord.HelperKeyWord;

namespace erlisp
{
    public class Parser
    {

        private static readonly List<IKeyWords> Functions = new List<IKeyWords>
        {
            new Write(),
            new IfStatment(),
            new Comparator(),
            new MatemticalOperator()
        };

        private static readonly List<IKeyWords> Expressions = new List<IKeyWords>
        {
            new Integers(),
            new Floats(),
            new Strings()
        };


        private static void RemoveElement(List<FoundKeyWord> skanedProgram)
        {
            CodeGenerator.AddNextToken(skanedProgram.First());
            skanedProgram.RemoveAt(0);

        }

        static bool IsFunction(List<FoundKeyWord> skanedProgram)
        {
            var keyWordName = skanedProgram.First().GetKeyWordName();

            if (Functions.All(x => x.KeyWordName() != keyWordName)) return false;
            RemoveElement(skanedProgram);

            while (IsAgrument(skanedProgram))
            {
                CodeGenerator.AddNextToken(new FoundKeyWord(",", new Comma()));
            }

            RemoveOptionalWhitespaces(skanedProgram);
            return skanedProgram.First().GetKeyWordName() == "ClosingBracket" ||
                   skanedProgram.First().GetKeyWordName() == "ClosingThread";

        }

        static bool IsAgrument(List<FoundKeyWord> skanedProgram)
        {
            RemoveOptionalWhitespaces(skanedProgram);

            return IsExpression(skanedProgram) || IsListOrThread(skanedProgram);
        }
        static bool IsExpression(List<FoundKeyWord> skanedProgram)
        {

            var keyWordName = skanedProgram.First().GetKeyWordName();
            if (Expressions.Any(x => x.KeyWordName() == keyWordName))
            {
                RemoveElement(skanedProgram);
                return true;
            }

            return false;
        }

        static bool IsListOrThread(List<FoundKeyWord> skanedProgram)
        {
            RemoveOptionalWhitespaces(skanedProgram);

            if (skanedProgram.First().GetKeyWordName() != "OpeningBracket" &&
                skanedProgram.First().GetKeyWordName() != "OpeningThread") return false;
            RemoveElement(skanedProgram);
            RemoveOptionalWhitespaces(skanedProgram);

            if (skanedProgram.First().GetKeyWordName() == "ClosingBracket" ||
                skanedProgram.First().GetKeyWordName() == "ClosingThred")
            {
                RemoveElement(skanedProgram);
                return true;
            }

            if (!IsFunction(skanedProgram)) return false;

            RemoveOptionalWhitespaces(skanedProgram);
            if (skanedProgram.First().GetKeyWordName() != "ClosingBracket" &&
                skanedProgram.First().GetKeyWordName() != "ClosingThread") return false;
            RemoveElement(skanedProgram);
            return true;
        }

        static void RemoveOptionalWhitespaces(List<FoundKeyWord> skanedProgram)
        {
            if (skanedProgram.First().GetKeyWordName() == "WhiteSpaces") RemoveElement(skanedProgram);
        }

        static bool IsInLineErlang(List<FoundKeyWord> skanedProgram)
        {
            RemoveOptionalWhitespaces(skanedProgram);
            if (skanedProgram.First().GetKeyWordName() != "InLineErlang") return false;
            RemoveElement(skanedProgram);
            return true;
        }

        public static bool Parse(List<FoundKeyWord> skanedProgram)
        {
            while (skanedProgram.Any())
            {
                
                if (!IsInLineErlang(skanedProgram) && !IsListOrThread(skanedProgram))
                    return false;
                CodeGenerator.AddNextToken(new FoundKeyWord(".", new EndOfCode()));
            }

            CodeGenerator.Reprocess();
            return true;
        }
    }
}