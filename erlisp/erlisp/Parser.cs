using System.Collections.Generic;
using System.Linq;
using erlisp.IKeyWord;

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
            }

            RemoveOptionalWhitespaces(skanedProgram);
            return skanedProgram[0].GetKeyWordName() == "ClosingBracket";

        }

        static bool IsAgrument(List<FoundKeyWord> skanedProgram)
        {
            RemoveOptionalWhitespaces(skanedProgram);

            return IsExpression(skanedProgram) || IsList(skanedProgram) || IsVariable(skanedProgram);
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

        static bool IsFunctionDef(List<FoundKeyWord> skanedProgram)
        {
            if (skanedProgram[0].GetKeyWordName() != "OpeningBracket") return false;
            RemoveElement(skanedProgram);
            RemoveOptionalWhitespaces(skanedProgram);

            if (skanedProgram[0].GetKeyWordName() != "Function") return false;
            RemoveElement(skanedProgram);
            RemoveOptionalWhitespaces(skanedProgram);

            while (IsVariable(skanedProgram))
            {
            }

            if (IsList(skanedProgram) &&
                IsList(skanedProgram) &&
                skanedProgram[0].GetKeyWordName() == "ClosingBracket")
            {
                RemoveElement(skanedProgram);
                return true;
            }

            return false;
        }

        static bool IsVariable(List<FoundKeyWord> skanedProgram)
        {
            RemoveOptionalWhitespaces(skanedProgram);
            if (skanedProgram[0].KeyWordType.KeyWordName() == "Variable")
            {
                RemoveElement(skanedProgram);
                return true;
            }

            return false;
        }

        static bool IsList(List<FoundKeyWord> skanedProgram)
        {
            RemoveOptionalWhitespaces(skanedProgram);

            if (skanedProgram[0].GetKeyWordName() != "OpeningBracket") return false;
            RemoveElement(skanedProgram);
            RemoveOptionalWhitespaces(skanedProgram);

            if (skanedProgram[0].GetKeyWordName() == "ClosingBracket")
            {
                RemoveElement(skanedProgram);
                return true;
            }

            if (!IsFunction(skanedProgram) && !IsVariable(skanedProgram)) return false;
            RemoveOptionalWhitespaces(skanedProgram);
            while (IsVariable(skanedProgram))
            {}

            if (skanedProgram[0].GetKeyWordName() == "ClosingBracket")
            {
                RemoveElement(skanedProgram);
                return true;
            }
            return false;
        }

        static void RemoveOptionalWhitespaces(List<FoundKeyWord> skanedProgram)
        {
            if (skanedProgram[0].GetKeyWordName() == "WhiteSpaces") RemoveElement(skanedProgram);
        }

        public static bool Parse(List<FoundKeyWord> skanedProgram)
        {
            while (skanedProgram.Any())
            {
                var lcp = new List<FoundKeyWord>(skanedProgram);

                if (IsFunctionDef(lcp))
                {
                    skanedProgram = lcp;
                    continue;
                }
                if (!IsList(skanedProgram)) return false;
            }

            CodeGenerator.Reprocess();
            return true;
        }
    }
}