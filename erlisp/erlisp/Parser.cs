using System.Collections.Generic;
using System.Linq;
using erlisp.IKeyWord;

namespace erlisp
{
    public class Parser
    {
        private static readonly List<IFunction> NonFoldFunctions = new List<IFunction>
        {
            new Write(),
            new IfStatment()
        };

        private static readonly List<IKeyWords> FoldFunctions = new List<IKeyWords>
        {
            new Comparator(),
            new MatemticalOperator()
        };

        private static readonly List<IKeyWords> Expressions = new List<IKeyWords>
        {
            new Integers(),
            new Floats(),
            new Strings()
        };

        static bool IsFunction(List<FoundKeyWord> skanedProgram)
        {
            return IsNonFoldFunction(skanedProgram) || IsFoldFunction(skanedProgram);

        }

        private static bool IsFoldFunction(List<FoundKeyWord> skanedProgram)
        {
            var keyWordName = skanedProgram[0].GetKeyWordName();

            if (FoldFunctions.All(x => x.KeyWordName() != keyWordName)) return false;
            skanedProgram.RemoveAt(0);

            while (IsAgrument(skanedProgram)){}

            RemoveOptionalWhitespaces(skanedProgram);
            return skanedProgram[0].GetKeyWordName() == "ClosingBracket";

        }

        private static bool IsNonFoldFunction(List<FoundKeyWord> skanedProgram)
        {
            var keyWordName = skanedProgram[0].GetKeyWordName();
            var numberOfArguments = -1;
            foreach (var function in NonFoldFunctions)
            {
                if (function.KeyWordName() == keyWordName)
                {
                    numberOfArguments = function.NuberOfArguments();
                }
            }

            if (numberOfArguments != -1)
            {
                skanedProgram.RemoveAt(0);
                for (int i = 0; i < numberOfArguments; i++)
                {
                    if (!IsAgrument(skanedProgram)) return false;
                }

                return true;
            }

            return false;
        }

        static bool IsAgrument(List<FoundKeyWord> skanedProgram)
        {
            RemoveOptionalWhitespaces(skanedProgram);

            return IsExpression(skanedProgram) || IsList(skanedProgram);
        }
        static bool IsExpression(List<FoundKeyWord> skanedProgram)
        {

            var keyWordName = skanedProgram[0].GetKeyWordName();
            if (Expressions.Any(x => x.KeyWordName() == keyWordName))
            {
                skanedProgram.RemoveAt(0);
                return true;
            }

            return false;
        }

        static bool IsList(List<FoundKeyWord> skanedProgram)
        {
            RemoveOptionalWhitespaces(skanedProgram);

            if (skanedProgram[0].GetKeyWordName() != "OpeningBracket") return false;
            skanedProgram.RemoveAt(0);
            RemoveOptionalWhitespaces(skanedProgram);

            if (skanedProgram[0].GetKeyWordName() == "ClosingBracket")
            {
                skanedProgram.RemoveAt(0);
                return true;
            }

            if (!IsFunction(skanedProgram)) return false;

            RemoveOptionalWhitespaces(skanedProgram);
            if (skanedProgram[0].GetKeyWordName() == "ClosingBracket")
            {
                skanedProgram.RemoveAt(0);
                return true;
            }
            return false;
        }

        static void RemoveOptionalWhitespaces(List<FoundKeyWord> skanedProgram)
        {
            if (skanedProgram[0].GetKeyWordName() == "WhiteSpaces") skanedProgram.RemoveAt(0);
        }

        public static bool Parse(List<FoundKeyWord> skanedProgram)
        {
            while (skanedProgram.Any())
            {
                if (!IsList(skanedProgram)) return false;
            }
            return true;
        }
    }
}