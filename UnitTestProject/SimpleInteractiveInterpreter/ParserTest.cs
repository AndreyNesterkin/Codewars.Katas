using System;
using Codewars.Katas.SimpleInteractiveInterpreter;
using Codewars.Katas.SimpleInteractiveInterpreter.Ast;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject.SimpleInteractiveInterpreter
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void DoubleConstParseShouldReturnDoubleConstAstNode()
        {
            var lexer = SetupDoubleConst("123");

            var parser = new Parser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(DoubleConstAstNode));
        }

        private ILexer SetupDoubleConst(string text)
        {
            var lexerMock = new Mock<ILexer>();

            lexerMock.Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, text));

            return lexerMock.Object;
        }

        [TestMethod]
        public void PlusParseShouldReturnBinOpAstNode()
        {
            var lexer = SetupPlus("1", "2");

            var parser = new Parser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(BinaryOperationAstNode));
            Assert.AreEqual(astNode.Token.Value, "+");
        }

        private ILexer SetupPlus(string lOp, string rOp)
        {
            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, lOp));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Plus, "+"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, rOp));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }

        [TestMethod]
        public void MinusParseShouldReturnBinOpAstNode()
        {
            var lexer = SetupMinus("1", "2");

            var parser = new Parser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(BinaryOperationAstNode));
            Assert.AreEqual(astNode.Token.Value, "-");
        }

        private ILexer SetupMinus(string lOp, string rOp)
        {
            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, lOp));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Minus, "-"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, rOp));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }

        [TestMethod]
        public void MultiplicationParseShouldReturnBinOpAstNode()
        {
            var lexer = SetupMultiplication("1", "2");

            var parser = new Parser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(BinaryOperationAstNode));
            Assert.AreEqual(astNode.Token.Value, "*");
        }

        private ILexer SetupMultiplication(string lOp, string rOp)
        {
            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, lOp));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Multiplication, "*"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, rOp));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }

        [TestMethod]
        public void DivisionParseShouldReturnBinOpAstNode()
        {
            var lexer = SetupDivision("1", "2");

            var parser = new Parser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(BinaryOperationAstNode));
            Assert.AreEqual(astNode.Token.Value, "/");
        }

        private ILexer SetupDivision(string lOp, string rOp)
        {
            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, lOp));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Division, "/"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, rOp));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }

        [TestMethod]
        public void DivisionRemainderParseShouldReturnBinOpAstNode()
        {
            var lexer = SetupDivisionRemainder("1", "2");

            var parser = new Parser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(BinaryOperationAstNode));
            Assert.AreEqual(astNode.Token.Value, "%");
        }

        private ILexer SetupDivisionRemainder(string lOp, string rOp)
        {
            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, lOp));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DivisionRemainder, "%"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, rOp));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }

        [TestMethod]
        public void EmptyParseShouldReturnEmptyAstNode()
        {
            var lexer = SetupEmpty();

            var parser = new Parser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(EmptyAstNode));
        }

        private ILexer SetupEmpty()
        {
            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            lexerMock.Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }

        [TestMethod]
        public void ParenthesesShouldReturnParenthesesAstNodes()
        {
            var lexer = SetupParentheses();

            var parser = new Parser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(DoubleConstAstNode));
        }

        private ILexer SetupParentheses()
        {
            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.LeftParenthesis, "("));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, "1"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.RightParenthesis, ")"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }

        [TestMethod]
        public void Expression1ShouldReturnAstNodes()
        {
            var lexer = SetupExpression1();

            var parser = new Parser(lexer);

            var astNode = parser.Parse();

            Assert.AreEqual("(2 * (1 + 1))", astNode.ToString());
        }

        private ILexer SetupExpression1()
        {
            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, 2d));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Multiplication, "*"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.LeftParenthesis, "("));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, 1d));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Plus, "+"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, 1d));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.RightParenthesis, ")"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }

        [TestMethod]
        public void IdentifierShouldReturnIdentifierAstNode()
        {
            var lexer = SetupIdentifier();

            var parser = new Parser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(IdentifierAstNode));
        }

        private ILexer SetupIdentifier()
        {
            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Identifier, "var3"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }

        [TestMethod]
        public void AssignmentShouldReturnAssignmentAstNode()
        {
            var lexer = SetupAssignment();

            var parser = new Parser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(AssignmentAstNode));
        }

        private ILexer SetupAssignment()
        {
            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Identifier, "a"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Assignment, "="));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, 1d));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }
    }
}
