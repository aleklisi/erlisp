using erlisp;
using NUnit.Framework;

namespace ScanerTests
{
    public class ParserTests
    {
        [TestCase("(+)")]
        [TestCase("(+ 1)")]
        [TestCase("(+   1   )")]
        [TestCase("(+1 )")]
        [TestCase("(+ 1 2   3)")]

        [TestCase("()")]
        [TestCase("( )")]

        [TestCase("(write 1)")]
        [TestCase("(write (+ 1 2   3))")]

        [TestCase("(if 1 2 3)")]
        [TestCase("(if (+ 1 2) 1 2)")]
        [TestCase("(if ()()())")]
        [TestCase("(if () 1 (+ 2 3))")]
        [TestCase("(if ()()(+))")]
        [TestCase("(if ()1())")]

        [TestCase("[write 3]")]
        [TestCase("[if 1 2 3]")]
        [TestCase("[if (+ 1 2 3) (write 6) (write 7)]")]
        [TestCase("[if (+ 1 2 3) [write 6] [write 7]]")]
        [TestCase("[if 1 2 3]")]
        [TestCase("[if 1 2 3]")]
        [TestCase("[+ 3]")]
        [TestCase("[+ [- 3]]")]
        [TestCase("[write [- 3]]")]
        [TestCase("[write 3][write 4]")]
        [TestCase("[write 3][write \"abc\"][write (+ 1 2 3)]")]

        public void TokenRecognizedCorrectly(string input)
        {
            var skaner = new LispScaner(input);

            var result = skaner.TokenizeInput();
            var parsed = Parser.Parse(result);
            Assert.IsTrue(parsed);
        }
    }
}