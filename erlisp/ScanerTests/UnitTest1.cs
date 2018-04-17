using System.Linq;
using erlisp;
using NUnit.Framework; 

namespace ScanerTests
{
    
    public class SkanerTests
    {
        [TestCase("\"\"", "Strings")]
        [TestCase("\"asdf\"", "Strings")]
        [TestCase("\"LHNIOYITU\"", "Strings")]
        [TestCase("\"123545\"", "Strings")]
        [TestCase("\"hbif 1234 DVLBIJ\t\n\"", "Strings")]
        public void TokenRecognizedCorrectly(string input, string tokenName)
        {
            var skaner = new LispScaner(input);

            var result = skaner.TokenizeInput();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.FirstOrDefault()?.GetKeyWordName() == tokenName);
        }
    }
}
