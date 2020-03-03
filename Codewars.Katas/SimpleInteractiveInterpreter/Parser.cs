using Codewars.Katas.SimpleInteractiveInterpreter.Ast;
using System;
using System.Collections.Generic;

namespace Codewars.Katas.SimpleInteractiveInterpreter
{
    public class Parser
    {
        private readonly ILexer _lexer;
        private readonly SymbolTable _symbolTable;
        private Token _currentToken;
        private string _currentScope;
        
        public Parser(ILexer lexer, SymbolTable symbolTable)
        {
            _lexer = lexer;
            _symbolTable = symbolTable;
        }

        public AstNode Parse()
        {
            _currentToken = _lexer.ReadNextToken();

            var node = ParseStatement();

            if (_currentToken.Type != TokenType.Eof)
                throw new InvalidOperationException("Failed to parse");

            return node;
        }

        private AstNode ParseStatement()
        {
            if (_currentToken.Type == TokenType.Eof)
                return new EmptyAstNode(_currentToken);

            if (_currentToken.Type == TokenType.FunctionDefinition)
            {
                var functionDefinition = ParseFunctionDefinition();
                _symbolTable.DefineFunction(functionDefinition.Name, functionDefinition);

                return functionDefinition;
            }

            return ParseExpression();
        }

        private FunctionDefinitionAstNode ParseFunctionDefinition()
        {
            Eat(TokenType.FunctionDefinition);

            var token = _currentToken;
            Eat(TokenType.Identifier);

            _currentScope = (string)token.Value;

            var arguments = ParseArguments();

            Eat(TokenType.FunctionOperator);

            var body = ParseExpression();

            _currentScope = null;

            return new FunctionDefinitionAstNode(token, arguments, body);
        }

        private AstNode[] ParseArguments()
        {
            var arguments = new List<AstNode>();

            while (_currentToken.Type != TokenType.FunctionOperator)
            {
                var argument = ParseIdentifier();
                arguments.Add(argument);

                _symbolTable.DefineVariable(_currentScope, argument.Name, argument);
            }

            return arguments.ToArray();
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

            if (_currentToken.Type == TokenType.Identifier)
            {
                var identifier = ParseIdentifier();

                if (_currentToken.Type == TokenType.Assignment)
                {
                    var assignment = ParseAssignment(identifier);
                    _symbolTable.DefineVariable(_currentScope, identifier.Name, identifier);

                    return assignment;
                }

                if (!(IsGlobalDefined(identifier) || IsLocalDefined(identifier)))
                    throw new InvalidOperationException($"Identificator {identifier.Name} is unknown");

                if (IsFunctionCall(identifier))
                    return ParseFunctionCall(identifier);

                return identifier;
            }

            return ParseParentheses();
        }

        private bool IsFunctionCall(IdentifierAstNode functionCall)
        {
            if (!IsGlobalDefined(functionCall))
                return false;

            var functionDefinition = _symbolTable.Lookup(functionCall.Name);

            return functionDefinition is FunctionDefinitionAstNode;
        }

        private AstNode ParseFunctionCall(IdentifierAstNode identifier)
        {
            var functionDefinition = (FunctionDefinitionAstNode)_symbolTable.Lookup(identifier.Name);

            var expressions = new List<AstNode>();

            var arguments = functionDefinition.Arguments;

            for (var i = 0; i < arguments.Length; i++)
                expressions.Add(ParseExpression());

            return new FunctionCallAstNode(identifier.Token, expressions.ToArray());
        }

        private bool IsLocalDefined(IdentifierAstNode identifier)
        {
            return _symbolTable.IsDefined(_currentScope, identifier.Name);
        }

        private bool IsGlobalDefined(IdentifierAstNode identifier)
        {
            return _symbolTable.IsDefined(identifier.Name);
        }

        private AstNode ParseParentheses()
        {
            Eat(TokenType.LeftParenthesis);
            var node = ParseExpression();
            Eat(TokenType.RightParenthesis);

            return node;
        }

        private AstNode ParseDoubleConst()
        {
            var node = new DoubleConstAstNode(_currentToken);

            Eat(TokenType.DoubleConst);

            return node;
        }

        private AstNode ParseBinaryOperation(AstNode leftOperand, Func<AstNode> parseRightOperand)
        {
            var operationToken = _currentToken;
            Eat(operationToken.Type);

            return new BinaryOperationAstNode(operationToken, leftOperand, parseRightOperand());
        }

        private void Eat(TokenType tokenType)
        {
            if (_currentToken.Type == tokenType)
                _currentToken = _lexer.ReadNextToken();
            else
                throw new InvalidOperationException("Invalid token");
        }

        private IdentifierAstNode ParseIdentifier()
        {
            var identifier = new IdentifierAstNode(_currentToken);

            Eat(TokenType.Identifier);

            return identifier;
        }

        private AstNode ParseAssignment(IdentifierAstNode variable)
        {
            var token = _currentToken;
            Eat(TokenType.Assignment);

            return new AssignmentAstNode(token, variable, ParseExpression());
        }
    }
}
