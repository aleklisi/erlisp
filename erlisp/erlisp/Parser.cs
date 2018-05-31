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
            var keyWordName = skanedProgram[0].GetKeyWordName();

            if (Functions.All(x => x.KeyWordName() != keyWordName)) return false;
            RemoveElement(skanedProgram);

            while (IsAgrument(skanedProgram))
            {
                CodeGenerator.AddNextToken(new FoundKeyWord(",", new Comma()));
            }

            RemoveOptionalWhitespaces(skanedProgram);
            return skanedProgram[0].GetKeyWordName() == "ClosingBracket" ||
                   skanedProgram[0].GetKeyWordName() == "ClosingThread";

        }

        static bool IsAgrument(List<FoundKeyWord> skanedProgram)
        {
            RemoveOptionalWhitespaces(skanedProgram);

            return IsExpression(skanedProgram) || IsListOrThread(skanedProgram);
        }
        static bool IsExpression(List<FoundKeyWord> skanedProgram)
        {

            var keyWordName = skanedProgram[0].GetKeyWordName();
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

            if (skanedProgram[0].GetKeyWordName() != "OpeningBracket" &&
                skanedProgram[0].GetKeyWordName() != "OpeningThread") return false;
            RemoveElement(skanedProgram);
            RemoveOptionalWhitespaces(skanedProgram);

            if (skanedProgram[0].GetKeyWordName() == "ClosingBracket" ||
                skanedProgram[0].GetKeyWordName() == "ClosingThred")
            {
                RemoveElement(skanedProgram);
                return true;
            }

            if (!IsFunction(skanedProgram)) return false;

            RemoveOptionalWhitespaces(skanedProgram);
            if (skanedProgram[0].GetKeyWordName() != "ClosingBracket" &&
                skanedProgram[0].GetKeyWordName() != "ClosingThread") return false;
            RemoveElement(skanedProgram);
            return true;
        }

        static void RemoveOptionalWhitespaces(List<FoundKeyWord> skanedProgram)
        {
            if (skanedProgram[0].GetKeyWordName() == "WhiteSpaces") RemoveElement(skanedProgram);
        }

        public static bool Parse(List<FoundKeyWord> skanedProgram)
        {
            while (skanedProgram.Any())
            {
                if (!IsListOrThread(skanedProgram))
                    return false;
                CodeGenerator.AddNextToken(new FoundKeyWord(".", new EndOfCode()));
            }

            CodeGenerator.Reprocess();
            return true;
        }
    }
}