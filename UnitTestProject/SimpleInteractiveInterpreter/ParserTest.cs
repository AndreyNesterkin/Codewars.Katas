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
        private SymbolTable _symbolTable;

        [TestInitialize]
        public void InitializeTest()
        {
            _symbolTable = new SymbolTable();
        }

        [TestMethod]
        public void DoubleConstParseShouldReturnDoubleConstAstNode()
        {
            var lexer = SetupDoubleConst("123");

            var parser = CreateParser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(DoubleConstAstNode));
        }

        private Parser CreateParser(ILexer lexer)
        {
            return new Parser(lexer, _symbolTable);
        }

        private ILexer SetupDoubleConst(string text)
        {
            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, text));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }

        [TestMethod]
        public void PlusParseShouldReturnBinOpAstNode()
        {
            var lexer = SetupPlus("1", "2");

            var parser = CreateParser(lexer);

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

            var parser = CreateParser(lexer);

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

            var parser = CreateParser(lexer);

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

            var parser = CreateParser(lexer);

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

            var parser = CreateParser(lexer);

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

            var parser = CreateParser(lexer);

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

            var parser = CreateParser(lexer);

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

            var parser = CreateParser(lexer);

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

            var parser = CreateParser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(IdentifierAstNode));
        }

        private ILexer SetupIdentifier()
        {
            _symbolTable.DefineVariable("var3", new IdentifierAstNode(new Token(TokenType.Identifier), "var3"));

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

            var parser = CreateParser(lexer);

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

        [TestMethod]
        public void FunctionDefinitionShouldReturnFunctionDefinitionAstNode()
        {
            var lexer = SetupFunctionDefinition();

            var parser = CreateParser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(FunctionDefinitionAstNode));

            var function = (FunctionDefinitionAstNode)astNode;
            Assert.AreEqual("echo", function.Name);

            Assert.AreEqual(1, function.Arguments.Length);
            var arugment = (IdentifierAstNode)function.Arguments[0];
            Assert.AreEqual("x", arugment.Name);

            var body = (IdentifierAstNode)function.Body;
            Assert.AreEqual("x", body.Name);
        }

        private ILexer SetupFunctionDefinition()
        {
            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.FunctionDefinition, "fn"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Identifier, "echo"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Identifier, "x"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.FunctionOperator, "=>"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Identifier, "x"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UsingNotDefinedVariableShouldThrowException()
        {
            var lexer = SetupUsingNotDefinedVariable();

            var parser = CreateParser(lexer);

            var astNode = parser.Parse();
        }

        private ILexer SetupUsingNotDefinedVariable()
        {
            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Identifier, "x"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Minus, "-"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, "1"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }

        [TestMethod]
        public void FunctionCallShouldReturnFunctionCallAstNode()
        {
            var lexer = SetupFunctionCall();

            var parser = CreateParser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(FunctionCallAstNode));

            var function = (FunctionCallAstNode)astNode;
            Assert.AreEqual("echo", function.Name);

            Assert.AreEqual(1, function.Arguments.Length);
            var arugment = (DoubleConstAstNode)function.Arguments[0];
            Assert.AreEqual(10d, arugment.Value);
        }

        private ILexer SetupFunctionCall()
        {
            var arguments = new[] { new IdentifierAstNode(new Token(TokenType.Identifier, "x"), "x") };
            var body = new EmptyAstNode(new Token(TokenType.Eof));
            _symbolTable.DefineFunction("echo", new FunctionDefinitionAstNode(new Token(TokenType.FunctionDefinition, "echo"), arguments, body));

            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Identifier, "echo"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, 10d));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }

        [TestMethod]
        public void FunctionCallWithTwoArgsShouldReturnFunctionCallAstNode()
        {
            var lexer = FunctionCallWithTwoArgs();

            var parser = CreateParser(lexer);

            var astNode = parser.Parse();

            Assert.IsInstanceOfType(astNode, typeof(FunctionCallAstNode));

            var function = (FunctionCallAstNode)astNode;
            Assert.AreEqual("avg", function.Name);

            Assert.AreEqual(2, function.Arguments.Length);
        }

        private ILexer FunctionCallWithTwoArgs()
        {
            var arguments = new[] {
                new IdentifierAstNode(new Token(TokenType.Identifier, "x"), "x"),
                new IdentifierAstNode(new Token(TokenType.Identifier, "y"), "y")
            };
            var body = new EmptyAstNode(new Token(TokenType.Eof));
            _symbolTable.DefineFunction("avg", new FunctionDefinitionAstNode(new Token(TokenType.FunctionDefinition, "avg"), arguments, body));

            var lexerMock = new Mock<ILexer>(MockBehavior.Strict);

            var s = new MockSequence();

            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Identifier, "avg"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, 1d));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Plus, "+"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, 2d));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, 3d));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Multiplication, "*"));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.DoubleConst, 4d));
            lexerMock.InSequence(s).Setup(t => t.ReadNextToken()).Returns(new Token(TokenType.Eof));

            return lexerMock.Object;
        }
    }
}
