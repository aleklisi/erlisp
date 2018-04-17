using System.Collections.Generic;
using System.Linq;
using erlisp.IKeyWord;

namespace erlisp
{
    class Parser
    {
        private static readonly List<IFunction> Functions = new List<IFunction>
        {
            new Write(),
            new IfStatment()
        };

        private static readonly List<IKeyWords> FunctionsRangeNumberOfArguments = new List<IKeyWords>
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
            var keyWordName = skanedProgram[0].KeyWordType.KeyWordName();
            var numberOfArguments = -1;
            foreach(var function in Functions)
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

            var keyWordName = skanedProgram[0].KeyWordType.KeyWordName();
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

            if (skanedProgram[0].KeyWordType.KeyWordName() != "OpeningBracket") return false;
            skanedProgram.RemoveAt(0);
            RemoveOptionalWhitespaces(skanedProgram);

            if (skanedProgram[0].KeyWordType.KeyWordName() == "ClosingBracket") return true;

            if (!IsFunction(skanedProgram)) return false;

            RemoveOptionalWhitespaces(skanedProgram);
            return skanedProgram[0].KeyWordType.KeyWordName() == "ClosingBracket";
        }

        static void RemoveOptionalWhitespaces(List<FoundKeyWord> skanedProgram)
        {
            if (skanedProgram[0].KeyWordType.KeyWordName() == "WhiteSpaces") skanedProgram.RemoveAt(0);
        }

        internal static bool Parse(List<FoundKeyWord> skanedProgram)
        {
            return IsList(skanedProgram);
        }
    }
}