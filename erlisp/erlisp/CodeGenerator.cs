using System.Collections.Generic;
using erlisp.IKeyWord;
using erlisp.IKeyWord.HelperKeyWord;

namespace erlisp
{
    internal class CodeGenerator
    {
        private static List<FoundKeyWord> tokenzedProgram = new List<FoundKeyWord>();

        public static void AddNextToken(FoundKeyWord token)
        {
            tokenzedProgram.Add(token);
        }

        public static List<FoundKeyWord> GetTokenizedProgram() => tokenzedProgram;

        public static void Reprocess()
        {
            RemoveExtraComma();
        }

        public static void RemoveExtraComma()
        {
            var first = new Comma();
            var second = new ClosingBracket();

            for (int i = 0; i < tokenzedProgram.Count - 2; i++)
            {
                if (tokenzedProgram[i].GetKeyWordName() == first.KeyWordName() &&
                    tokenzedProgram[i + 1].GetKeyWordName() == second.KeyWordName())
                {
                    tokenzedProgram.RemoveAt(i);
                }
            }
        }
    }
}