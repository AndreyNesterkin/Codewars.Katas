using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codewars.Katas.SimpleInteractiveInterpreter;
using System.Globalization;

namespace UnitTestProject.SimpleInteractiveInterpreter
{
    [TestClass]
    public class LexerTest
    {
        [TestMethod]
        public void EmptyTextGetNextTokenShouldReturnEofToken()
        {
            var text = "";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();

            Assert.AreEqual(TokenType.Eof, token.Type);
        }

        [TestMethod]
        public void IntegerGetNextTokenShouldReturnDoubleConstToken()
        {
            var text = "136";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();

            Assert.AreEqual(TokenType.DoubleConst, token.Type);
            Assert.AreEqual(double.Parse(text), (double)token.Value);

            token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.Eof, token.Type);
        }

        private Lexer SetupLexer(string text)
        {
            var lexer = new Lexer();

            lexer.SetText(text);

            return lexer;
        }

        [TestMethod]
        public void DoubleGetNextTokenShouldReturnDoubleConstToken()
        {
            var text = "136.93";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();

            Assert.AreEqual(TokenType.DoubleConst, token.Type);
            Assert.AreEqual(double.Parse(text, CultureInfo.InvariantCulture), (double)token.Value);

            token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.Eof, token.Type);
        }

        [TestMethod]
        public void PlusGetNextTokenShouldReturnPlusToken()
        {
            var text = "+";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();

            Assert.AreEqual(TokenType.Plus, token.Type);
            Assert.AreEqual("+", token.Value);

            token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.Eof, token.Type);
        }

        [TestMethod]
        public void MinusGetNextTokenShouldReturnMinusToken()
        {
            var text = "-";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();

            Assert.AreEqual(TokenType.Minus, token.Type);
            Assert.AreEqual("-", token.Value);

            token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.Eof, token.Type);
        }

        [TestMethod]
        public void SpacesGetNextTokenShouldIgnoreSpaces()
        {
            var text = "  ";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();

            Assert.AreEqual(TokenType.Eof, token.Type);
        }

        [TestMethod]
        public void MultiplicationGetNextTokenShouldMultiplicationToken()
        {
            var text = "*";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();

            Assert.AreEqual(TokenType.Multiplication, token.Type);
            Assert.AreEqual("*", token.Value);

            token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.Eof, token.Type);
        }

        [TestMethod]
        public void DivisionGetNextTokenShouldDivisionToken()
        {
            var text = "/";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();

            Assert.AreEqual(TokenType.Division, token.Type);
            Assert.AreEqual("/", token.Value);

            token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.Eof, token.Type);
        }

        [TestMethod]
        public void DivisionRemainderGetNextTokenShouldDivisionRemainderToken()
        {
            var text = "%";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();

            Assert.AreEqual(TokenType.DivisionRemainder, token.Type);
            Assert.AreEqual("%", token.Value);

            token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.Eof, token.Type);
        }
    }
}
