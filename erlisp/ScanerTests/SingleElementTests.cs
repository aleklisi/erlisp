using System.Linq;
using erlisp;
using NUnit.Framework; 

namespace ScanerTests
{
    public class SkanerTests
    {
        [TestCase("\"\"", "Strings")]
        [TestCase("\"   \"", "Strings")]
        [TestCase("\"\n\n\"", "Strings")]
        [TestCase("\"\nasdf\n\"", "Strings")]
        [TestCase("\"\t\"", "Strings")]
        [TestCase("\"\n\t \"", "Strings")]
        [TestCase("\"  \t\t\n\n\"", "Strings")]
        [TestCase("\"1234\"", "Strings")]
        [TestCase("\"asdf\"", "Strings")]
        [TestCase("\"LHNIOYITU\"", "Strings")]
        [TestCase("\"123545\"", "Strings")]
        [TestCase("\"hbif 1234 DVLBIJ\t\n\"", "Strings")]

        [TestCase("1234", "Integer")]
        [TestCase("0", "Integer")]
        [TestCase("001123", "Integer")]

        [TestCase("1234.9", "Float")]
        [TestCase("0.21", "Float")]
        [TestCase("0.00", "Float")]
        [TestCase("12.54", "Float")]

        [TestCase(" ", "WhiteSpaces")]
        [TestCase("   ", "WhiteSpaces")]
        [TestCase("\t", "WhiteSpaces")]
        [TestCase("\t\t\t\t ", "WhiteSpaces")]
        [TestCase("\n\n\n\n", "WhiteSpaces")]

        [TestCase("(", "OpeningBracket")]

        [TestCase("[", "OpeningThread")]

        [TestCase(")", "ClosingBracket")]

        [TestCase("]", "ClosingThread")]

        [TestCase("+", "MatemticalOperator")]
        [TestCase("-", "MatemticalOperator")]
        [TestCase("/", "MatemticalOperator")]
        [TestCase("*", "MatemticalOperator")]

        [TestCase(">", "Comparator")]
        [TestCase("=", "Comparator")]
        [TestCase("<", "Comparator")]
        [TestCase(">=", "Comparator")]
        [TestCase("<=", "Comparator")]

        [TestCase("if", "IfStatment")]
        [TestCase("If", "IfStatment")]
        [TestCase("IF", "IfStatment")]

        [TestCase("write", "Write")]

        [TestCase("@@", "InLineErlang")]
        [TestCase("@example(3) -> X - 1.@", "InLineErlang")]
        [TestCase("@example(3) -> X - 1; \nexample(X) -> X + 2.@", "InLineErlang")]



        public void TokenRecognizedCorrectly(string input, string tokenName)
        {
            var skaner = new LispScaner(input);

            var result = skaner.TokenizeInput();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.FirstOrDefault()?.GetKeyWordName() == tokenName);
        }
    }
}
