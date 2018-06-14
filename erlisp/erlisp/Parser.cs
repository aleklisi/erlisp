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
        public static FoundKeyWord CurrentSymbol;

        private static void RemoveElement()
        {
            CodeGenerator.AddNextToken(CurrentSymbol);
            SkanedProgram.RemoveAt(0);
            CurrentSymbol = SkanedProgram.FirstOrDefault();

        }

        static bool IsFunction()
        {
            var keyWordName = CurrentSymbol.GetKeyWordName();

            if (Functions.All(x => x.KeyWordName() != keyWordName)) return false;
            RemoveElement();

            while (IsAgrument())
            {
                CodeGenerator.AddNextToken(new FoundKeyWord(",", new Comma()));
            }

            RemoveOptionalWhitespaces();
            return CurrentSymbol.GetKeyWordName() == "ClosingBracket" ||
                   CurrentSymbol.GetKeyWordName() == "ClosingThread";

        }

        static bool IsAgrument()
        {
            RemoveOptionalWhitespaces();

            return IsExpression() || IsListOrThread();
        }
        static bool IsExpression()
        {

            var keyWordName = CurrentSymbol.GetKeyWordName();
            if (Expressions.All(x => x.KeyWordName() != keyWordName)) return false;
            RemoveElement();
            return true;

        }

        static bool IsListOrThread()
        {
            RemoveOptionalWhitespaces();

            if (CurrentSymbol.GetKeyWordName() != "OpeningBracket" &&
                CurrentSymbol.GetKeyWordName() != "OpeningThread") return false;
            RemoveElement();
            RemoveOptionalWhitespaces();

            if (CurrentSymbol.GetKeyWordName() == "ClosingBracket" ||
                CurrentSymbol.GetKeyWordName() == "ClosingThred")
            {
                RemoveElement();
                return true;
            }

            if (!IsFunction()) return false;

            RemoveOptionalWhitespaces();
            if (CurrentSymbol.GetKeyWordName() != "ClosingBracket" &&
                CurrentSymbol.GetKeyWordName() != "ClosingThread") return false;
            RemoveElement();
            return true;
        }

        static void RemoveOptionalWhitespaces()
        {
            if (CurrentSymbol.GetKeyWordName() == "WhiteSpaces") RemoveElement();
        }

        static bool IsInLineErlang()
        {
            RemoveOptionalWhitespaces();
            if (CurrentSymbol.GetKeyWordName() != "InLineErlang") return false;
            RemoveElement();
            return true;
        }

        public static bool Parse(List<FoundKeyWord> inputProgram)
        {
            SkanedProgram = inputProgram;
            CurrentSymbol = inputProgram.FirstOrDefault();

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