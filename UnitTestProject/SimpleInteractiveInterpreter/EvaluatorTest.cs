using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codewars.Katas.SimpleInteractiveInterpreter;
using Codewars.Katas.SimpleInteractiveInterpreter.Ast;

namespace UnitTestProject.SimpleInteractiveInterpreter
{
    [TestClass]
    public class EvaluatorTest
    {
        private SymbolTable _symbolTable;

        [TestInitialize]
        public void InitializeTest()
        {
            _symbolTable = new SymbolTable();
        }

        [TestMethod]
        public void DoubleConstEvaluateShouldComputeDoubleConst()
        {
            var node = SetupDoubleConst();

            var eval = CreateEvaluator();

            var result = eval.Evaluate(node);

            Assert.AreEqual(1.5d, result);
        }

        private AstNode SetupDoubleConst()
        {
            return new DoubleConstAstNode(new Token(TokenType.DoubleConst, 1.5d));
        }

        [TestMethod]
        public void AddOperationEvaluateShouldComputeSum()
        {
            var node = SetupAddOperation();

            var eval = CreateEvaluator();

            var result = eval.Evaluate(node);

            Assert.AreEqual(2.5d, result);
        }

        private AstNode SetupAddOperation()
        {
            var token = new Token(TokenType.Plus);

            var leftOperand = new DoubleConstAstNode(new Token(TokenType.DoubleConst, 1d));

            var rightOperand = new DoubleConstAstNode(new Token(TokenType.DoubleConst, 1.5d));

            return new BinaryOperationAstNode(token, leftOperand, rightOperand);
        }

        [TestMethod]
        public void MinusOperationEvaluateShouldComputeResult()
        {
            var node = SetupMinusOperation();

            var eval = CreateEvaluator();

            var result = eval.Evaluate(node);

            Assert.AreEqual(-0.5d, result);
        }

        private AstNode SetupMinusOperation()
        {
            var token = new Token(TokenType.Minus);

            var leftOperand = new DoubleConstAstNode(new Token(TokenType.DoubleConst, 1d));

            var rightOperand = new DoubleConstAstNode(new Token(TokenType.DoubleConst, 1.5d));

            return new BinaryOperationAstNode(token, leftOperand, rightOperand);
        }

        [TestMethod]
        public void MultiplicationOperationEvaluateShouldComputeResult()
        {
            var node = SetupMultiplicationOperation();

            var eval = CreateEvaluator();

            var result = eval.Evaluate(node);

            Assert.AreEqual(4d, result);
        }

        private AstNode SetupMultiplicationOperation()
        {
            var token = new Token(TokenType.Multiplication);

            var leftOperand = new DoubleConstAstNode(new Token(TokenType.DoubleConst, 2d));

            var rightOperand = new DoubleConstAstNode(new Token(TokenType.DoubleConst, 2d));

            return new BinaryOperationAstNode(token, leftOperand, rightOperand);
        }

        [TestMethod]
        public void DivisionOperationEvaluateShouldComputeResult()
        {
            var node = SetupDivisionOperation();

            var eval = CreateEvaluator();

            var result = eval.Evaluate(node);

            Assert.AreEqual(1d, result);
        }

        private AstNode SetupDivisionOperation()
        {
            var token = new Token(TokenType.Division);

            var leftOperand = new DoubleConstAstNode(new Token(TokenType.DoubleConst, 2d));

            var rightOperand = new DoubleConstAstNode(new Token(TokenType.DoubleConst, 2d));

            return new BinaryOperationAstNode(token, leftOperand, rightOperand);
        }

        [TestMethod]
        public void DivisionRemainderOperationEvaluateShouldComputeResult()
        {
            var node = SetupDivisionRemainderOperation();

            var eval = CreateEvaluator();

            var result = eval.Evaluate(node);

            Assert.AreEqual(0d, result);
        }

        private AstNode SetupDivisionRemainderOperation()
        {
            var token = new Token(TokenType.DivisionRemainder);

            var leftOperand = new DoubleConstAstNode(new Token(TokenType.DoubleConst, 2d));

            var rightOperand = new DoubleConstAstNode(new Token(TokenType.DoubleConst, 2d));

            return new BinaryOperationAstNode(token, leftOperand, rightOperand);
        }

        [TestMethod]
        public void EmptyEvaluateShouldReturnNull()
        {
            var node = SetupEmpty();

            var eval = CreateEvaluator();

            var result = eval.Evaluate(node);

            Assert.IsNull(result);
        }

        private AstNode SetupEmpty()
        {
            return new EmptyAstNode(new Token(TokenType.Eof));
        }

        [TestMethod]
        public void AssignmentEvaluateShouldReturnComputeResult()
        {
            var node = SetupAssignment();

            var eval = CreateEvaluator();

            var result = eval.Evaluate(node);

            Assert.AreEqual(4d, result);
        }

        private AstNode SetupAssignment()
        {
            var token = new Token(TokenType.Assignment);

            var variable = new IdentifierAstNode(new Token(TokenType.Identifier, "a"), "a");

            var expr = new DoubleConstAstNode(new Token(TokenType.DoubleConst, 4d));

            return new AssignmentAstNode(token, variable, expr);
        }

        [TestMethod]
        public void OutVariableEvaluateShouldReturnVariableValue()
        {
            var eval = CreateEvaluator();

            eval.Evaluate(SetupAssignment());

            var node = SetupOutVariable();
            var result = eval.Evaluate(node);

            Assert.AreEqual(4d, result);
        }

        private AstNode SetupOutVariable()
        {
            return new IdentifierAstNode(new Token(TokenType.Identifier, "a"), "a");
        }

        [TestMethod]
        public void FunctionCallEvaluateShouldReturnResult()
        {
            var node = SetupFunctionCall();

            var eval = CreateEvaluator();
            
            var result = eval.Evaluate(node);

            Assert.AreEqual(12d, result);
        }

        private AstNode SetupFunctionCall()
        {
            var functionDefinition = SetupFunctionDefinition2();

            _symbolTable.DefineFunction("sum", functionDefinition);

            var arguments = new[]
            {
                new DoubleConstAstNode(new Token(TokenType.DoubleConst, 6d)),
                new DoubleConstAstNode(new Token(TokenType.DoubleConst, 6d)),
            };

            return new FunctionCallAstNode(new Token(TokenType.Identifier, "sum"), arguments);
        }

        private FunctionDefinitionAstNode SetupFunctionDefinition2()
        {
            var arguments = new[]
           {
                new IdentifierAstNode(new Token(TokenType.Identifier, "x"), "x"),
                new IdentifierAstNode(new Token(TokenType.Identifier, "y"), "y"),
            };

            var body = new BinaryOperationAstNode(new Token(TokenType.Plus, "+"), arguments[0], arguments[1]);

            return new FunctionDefinitionAstNode(new Token(TokenType.Identifier, "sum"), arguments, body);
        }

        private Evaluator CreateEvaluator()
        {
            return new Evaluator(_symbolTable);
        }

        [TestMethod]
        public void FunctionDefinitionEvaluateShouldReturnNull()
        {
            var node = SetupFunctionDefinition();

            var eval = CreateEvaluator();

            var result = eval.Evaluate(node);

            Assert.IsNull(result);
        }

        private AstNode SetupFunctionDefinition()
        {
            var arguments = new[]
            {
                new IdentifierAstNode(new Token(TokenType.Identifier, "a"), "a"),
            };

            var body = new IdentifierAstNode(new Token(TokenType.Identifier, "a"), "a");

            return new FunctionDefinitionAstNode(new Token(TokenType.Identifier, "echo"), arguments, body);
        }
    }
}
