using System;
using erlisp;
using NUnit.Framework;
using CodeGenerator = erlisp.CodeGenerator;

namespace ScanerTests
{
    public class CodeGeneratorTests
    {
        [TestCase("(+)", "fun1() -> sum([]).")]
        [TestCase("(+ 1     2   3)", "fun1() -> sum([1,2,3]).")]
        [TestCase("(+ 1 2 3)", "fun1() -> sum([1,2,3]).")]
        [TestCase("(+ 1)", "fun1() -> sum([1]).")]

        public void TokenRecognizedCorrectly(string input, string expectedCode)
        {
            var skaner = new LispScaner(input);

            var tokenized = skaner.TokenizeInput();
            Parser.Parse(tokenized);
            var generated = CodeGenerator.GenerateCode().Replace("\n", String.Empty).Replace("\r", String.Empty).Replace("\t", String.Empty);
            Assert.True(generated == expectedCode);
        }

        [TearDown]
        public void CleanUpStaticElements()
        {
            CodeGenerator.StackCounter = 0;
            CodeGenerator.FunCounter = 0;
            CodeGenerator._tokenzedProgram = new System.Collections.Generic.List<FoundKeyWord>();
        }
    }
}