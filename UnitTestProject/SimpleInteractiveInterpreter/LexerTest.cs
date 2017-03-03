using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codewars.Katas.SimpleInteractiveInterpreter;
using System.Globalization;
using System;

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
        public void MultiplicationGetNextTokenShouldReturnMultiplicationToken()
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
        public void DivisionGetNextTokenShouldReturnDivisionToken()
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
        public void DivisionRemainderGetNextTokenShouldReturnDivisionRemainderToken()
        {
            var text = "%";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();

            Assert.AreEqual(TokenType.DivisionRemainder, token.Type);
            Assert.AreEqual("%", token.Value);

            token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.Eof, token.Type);
        }

        [TestMethod]
        public void ParentheseGetNextTokenShouldReturnParenthesesTokens()
        {
            var text = "()";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.LeftParenthesis, token.Type);
            Assert.AreEqual("(", token.Value);

            token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.RightParenthesis, token.Type);
            Assert.AreEqual(")", token.Value);

            token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.Eof, token.Type);
        }

        [TestMethod]
        public void IdentifierGetNextTokenShouldReturnIdentifierToken()
        {
            var text = "variable_12";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.Identifier, token.Type);
            Assert.AreEqual("variable_12", token.Value);

            token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.Eof, token.Type);
        }

        [TestMethod]
        public void InvalidIdentifierGetNextTokenShouldReturnWrongToken()
        {
            var text = "12variable";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();

            Assert.AreEqual(TokenType.DoubleConst, token.Type);
        }

        [TestMethod]
        public void AssignmentGetNextTokenShouldReturnAssignmentToken()
        {
            var text = "=";

            var lexer = SetupLexer(text);

            var token = lexer.ReadNextToken();

            Assert.AreEqual(TokenType.Assignment, token.Type);
            Assert.AreEqual("=", token.Value);

            token = lexer.ReadNextToken();
            Assert.AreEqual(TokenType.Eof, token.Type);
        }
    }
}
