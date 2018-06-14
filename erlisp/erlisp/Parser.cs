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
        public static List<FoundKeyWord> SkanedProgram = new List<FoundKeyWord>();

        private static void RemoveElement()
        {
            CodeGenerator.AddNextToken(SkanedProgram.First());
            SkanedProgram.RemoveAt(0);

        }

        static bool IsFunction()
        {
            var keyWordName = SkanedProgram.First().GetKeyWordName();

            if (Functions.All(x => x.KeyWordName() != keyWordName)) return false;
            RemoveElement();

            while (IsAgrument())
            {
                CodeGenerator.AddNextToken(new FoundKeyWord(",", new Comma()));
            }

            RemoveOptionalWhitespaces();
            return SkanedProgram.First().GetKeyWordName() == "ClosingBracket" ||
                   SkanedProgram.First().GetKeyWordName() == "ClosingThread";

        }

        static bool IsAgrument()
        {
            RemoveOptionalWhitespaces();

            return IsExpression() || IsListOrThread();
        }
        static bool IsExpression()
        {

            var keyWordName = SkanedProgram.First().GetKeyWordName();
            if (Expressions.Any(x => x.KeyWordName() == keyWordName))
            {
                RemoveElement();
                return true;
            }

            return false;
        }

        static bool IsListOrThread()
        {
            RemoveOptionalWhitespaces();

            if (SkanedProgram.First().GetKeyWordName() != "OpeningBracket" &&
                SkanedProgram.First().GetKeyWordName() != "OpeningThread") return false;
            RemoveElement();
            RemoveOptionalWhitespaces();

            if (SkanedProgram.First().GetKeyWordName() == "ClosingBracket" ||
                SkanedProgram.First().GetKeyWordName() == "ClosingThred")
            {
                RemoveElement();
                return true;
            }

            if (!IsFunction()) return false;

            RemoveOptionalWhitespaces();
            if (SkanedProgram.First().GetKeyWordName() != "ClosingBracket" &&
                SkanedProgram.First().GetKeyWordName() != "ClosingThread") return false;
            RemoveElement();
            return true;
        }

        static void RemoveOptionalWhitespaces()
        {
            if (SkanedProgram.First().GetKeyWordName() == "WhiteSpaces") RemoveElement();
        }

        static bool IsInLineErlang()
        {
            RemoveOptionalWhitespaces();
            if (SkanedProgram.First().GetKeyWordName() != "InLineErlang") return false;
            RemoveElement();
            return true;
        }

        public static bool Parse(List<FoundKeyWord> inputProgram)
        {
            SkanedProgram = inputProgram;
            while (SkanedProgram.Any())
            {
                
                if (!IsInLineErlang() && !IsListOrThread())
                    return false;
                CodeGenerator.AddNextToken(new FoundKeyWord(".", new EndOfCode()));
            }

            CodeGenerator.Reprocess();
            return true;
        }
    }
}