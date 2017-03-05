using Codewars.Katas.SimpleInteractiveInterpreter.Ast;
using System;
using System.Collections.Generic;

namespace Codewars.Katas.SimpleInteractiveInterpreter
{
    public class Parser
    {
        private ILexer _lexer;
        private SymbolTable _symbolTable;
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
                throw new InvalidOperationException();

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

            var function = _currentToken;
            Eat(TokenType.Identifier);

            _currentScope = (string)function.Value;

            var arguments = ParseArguments();

            var body = ParseExpression();

            _currentScope = null;

            return new FunctionDefinitionAstNode(function, arguments, body);
        }

        private AstNode[] ParseArguments()
        {
            var arguments = new List<AstNode>();

            while (_currentToken.Type != TokenType.FunctionOperator)
            {
                var argumentNode = ParseIdentifier();
                arguments.Add(argumentNode);

                _symbolTable.DefineVariable(GetFullName(argumentNode.Name), argumentNode);
            }

            Eat(TokenType.FunctionOperator);

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

            if (_currentToken.Type == TokenType.LeftParenthesis)
                return ParseParentheses();

            if (_currentToken.Type == TokenType.Identifier)
            {
                var identifier = ParseIdentifier();

                if (_currentToken.Type == TokenType.Assignment)
                {
                    var assignment = ParseAssignment(identifier);
                    _symbolTable.DefineVariable(GetFullName(identifier.Name), identifier);

                    return assignment;
                }

                if (!(IsGlobal(identifier) || IsLocal(identifier)))
                    throw new InvalidOperationException($"Identificator {identifier.Name} is unknown");

                if (IsFunctionCall(identifier))
                    return ParseFunctionCall(identifier);

                return identifier;
            }

            throw new InvalidOperationException("Invalid factor");
        }

        private bool IsFunctionCall(IdentifierAstNode identifier)
        {
            if (!IsGlobal(identifier))
                return false;

            var function = _symbolTable.Lookup(identifier.Name);

            return function is FunctionDefinitionAstNode;
        }

        private AstNode ParseFunctionCall(IdentifierAstNode identifier)
        {
            var function = (FunctionDefinitionAstNode)_symbolTable.Lookup(identifier.Name);

            var expressions = new List<AstNode>();

            var arguments = function.Arguments;

            for (var i = 0; i < arguments.Length; i++)
                expressions.Add(ParseExpression());

            return new FunctionCallAstNode(identifier.Token, expressions.ToArray());
        }

        private bool IsLocal(IdentifierAstNode identifier)
        {
            return _symbolTable.IsDefined(GetFullName(identifier.Name));
        }

        private bool IsGlobal(IdentifierAstNode identifier)
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
            var identifier = new IdentifierAstNode(_currentToken, (string)_currentToken.Value);

            Eat(TokenType.Identifier);

            return identifier;
        }

        private string GetFullName(string name)
        {
            return _currentScope == null ? name : _currentScope + "." + name;
        }

        private AstNode ParseAssignment(IdentifierAstNode variable)
        {
            var assignmentToken = _currentToken;
            Eat(TokenType.Assignment);

            return new AssignmentAstNode(assignmentToken, variable, ParseExpression());
        }
    }
}
