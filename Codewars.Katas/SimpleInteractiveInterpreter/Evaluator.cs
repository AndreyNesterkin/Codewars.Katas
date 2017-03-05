using System;
using Codewars.Katas.SimpleInteractiveInterpreter.Ast;

namespace Codewars.Katas.SimpleInteractiveInterpreter
{
    public class Evaluator
    {
        public double? Evaluate(AstNode node)
        {
            if (IsEmpty(node))
                return null;

            return EvaluateNode(node);
        }

        private bool IsEmpty(AstNode node)
        {
            return node is EmptyAstNode;
        }

        public double EvaluateNode(AstNode node)
        {
            if (IsDoubleConst(node))
                return EvaluateDoubleConst(node);

            if (IsBinaryOperation(node))
                return EvaluateBinaryOperation((BinaryOperationAstNode)node);

            throw new NotSupportedException();
        }

        private bool IsDoubleConst(AstNode node)
        {
            return node is DoubleConstAstNode;
        }

        private double EvaluateDoubleConst(AstNode node)
        {
            return ((DoubleConstAstNode)node).Value;
        }

        private double EvaluateBinaryOperation(BinaryOperationAstNode operation)
        {
            var type = operation.Token.Type;

            var x = EvaluateNode(operation.LeftOperand);
            var y = EvaluateNode(operation.RightOperand);

            if (type == TokenType.Plus)
                return x + y;
            else if (type == TokenType.Minus)
                return x - y;
            else if (type == TokenType.Multiplication)
                return x * y;
            else if (type == TokenType.Division)
                return x / y;
            else if (type == TokenType.DivisionRemainder)
                return x % y;
            else
                throw new NotSupportedException();
        }

        private bool IsBinaryOperation(AstNode node)
        {
            return node is BinaryOperationAstNode;
        }
    }
}
