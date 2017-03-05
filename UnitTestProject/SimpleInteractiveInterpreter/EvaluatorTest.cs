using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codewars.Katas.SimpleInteractiveInterpreter;
using Codewars.Katas.SimpleInteractiveInterpreter.Ast;

namespace UnitTestProject.SimpleInteractiveInterpreter
{
    [TestClass]
    public class EvaluatorTest
    {
        [TestMethod]
        public void DoubleConstEvaluateShouldComputeDoubleConst()
        {
            var node = SetupDoubleConst();

            var eval = new Evaluator();

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

            var eval = new Evaluator();

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

            var eval = new Evaluator();

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

            var eval = new Evaluator();

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

            var eval = new Evaluator();

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

            var eval = new Evaluator();

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

            var eval = new Evaluator();

            var result = eval.Evaluate(node);

            Assert.IsNull(result);
        }

        private AstNode SetupEmpty()
        {
            return new EmptyAstNode(new Token(TokenType.Eof));
        }
    }
}
