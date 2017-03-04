using Codewars.Katas.SimpleInteractiveInterpreter.Ast;
using System;

namespace Codewars.Katas.SimpleInteractiveInterpreter
{
    public class Parser
    {
        private ILexer _lexer;
        private Token _currentToken;

        public Parser(ILexer lexer)
        {
            _lexer = lexer;
        }

        public AstNode Parse()
        {
            _currentToken = _lexer.ReadNextToken();

            return ParseStatement();
        }

        private AstNode ParseStatement()
        {
            if (_currentToken.Type == TokenType.Eof)
                return new EmptyAstNode(_currentToken);

            return ParseExpression();
        }

        private AstNode ParseExpression()
        {
            var node = ParseTerm();

            while (_currentToken.Type == TokenType.Plus ||
                _currentToken.Type == TokenType.Minus)
            {
                node = ParseBinaryOperation(node, ParseTerm);
            }

            return node;
        }

        private AstNode ParseTerm()
        {
            var node = ParseFactor();

            while (_currentToken.Type == TokenType.Multiplication ||
                _currentToken.Type == TokenType.Division || 
                _currentToken.Type == TokenType.DivisionRemainder)
            {
                node = ParseBinaryOperation(node, ParseFactor);
            }

            return node;
        }

        private AstNode ParseFactor()
        {
            if (_currentToken.Type == TokenType.DoubleConst)
                return ParseDoubleConst();

            if (_currentToken.Type == TokenType.LeftParenthesis)
                return ParseParentheses();

            if (_currentToken.Type == TokenType.Identifier)
            {
                var node = ParseIdentifier();

                if (_currentToken.Type == TokenType.Assignment)
                    node = ParseAssignment(node);

                return node;
            }

            throw new InvalidOperationException("Invalid factor");
        }

        private AstNode ParseParentheses()
        {
            MoveNext(TokenType.LeftParenthesis);
            var node = ParseExpression();
            MoveNext(TokenType.RightParenthesis);

            return node;
        }

        private AstNode ParseDoubleConst()
        {
            var node = new DoubleConstAstNode(_currentToken);

            MoveNext(TokenType.DoubleConst);

            return node;
        }

        private AstNode ParseBinaryOperation(AstNode leftOperand, Func<AstNode> parseRightOperand)
        {
            var operationToken = _currentToken;
            MoveNext(operationToken.Type);

            return new BinaryOperationAstNode(operationToken, leftOperand, parseRightOperand());
        }

        private void MoveNext(TokenType tokenType)
        {
            if (_currentToken.Type == tokenType)
                _currentToken = _lexer.ReadNextToken();
            else
                throw new InvalidOperationException("Invalid token");
        }

        private AstNode ParseIdentifier()
        {
            var node = new IdentifierAstNode(_currentToken, (string)_currentToken.Value);

            MoveNext(TokenType.Identifier);

            return node;
        }

        private AstNode ParseAssignment(AstNode variable)
        {
            var assignmentToken = _currentToken;
            MoveNext(TokenType.Assignment);

            return new AssignmentAstNode(assignmentToken, variable, ParseExpression());
        }
    }
}
