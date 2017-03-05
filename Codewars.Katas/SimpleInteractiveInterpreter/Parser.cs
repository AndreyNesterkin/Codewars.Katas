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

            return ParseStatement();
        }

        private AstNode ParseStatement()
        {
            if (_currentToken.Type == TokenType.Eof)
                return new EmptyAstNode(_currentToken);

            if (_currentToken.Type == TokenType.FunctionDefinition)
            {
                var functionNode = ParseFunctionDefinition();
                _symbolTable.DefineFunction(functionNode.Name, functionNode);

                return functionNode;
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

                _symbolTable.DefineVariable(GetFullName(argumentNode.Identifier), argumentNode);
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
                var identifierNode = ParseIdentifier();

                if (_currentToken.Type == TokenType.Assignment)
                {
                    var assignmentNode = ParseAssignment(identifierNode);
                    _symbolTable.DefineVariable(GetFullName(identifierNode.Identifier), identifierNode);

                    return assignmentNode;
                }

                if (!(IsGlobalIdentifier(identifierNode) || IsLocalIdentifier(identifierNode)))
                    throw new InvalidOperationException($"Identificator {identifierNode.Identifier} is unknown");

                if (IsFunctionCall(identifierNode))
                    return ParseFunctionCall(identifierNode);

                return identifierNode;
            }

            throw new InvalidOperationException("Invalid factor");
        }

        private bool IsFunctionCall(IdentifierAstNode identifierNode)
        {
            if (!IsGlobalIdentifier(identifierNode))
                return false;

            var node = _symbolTable.Lookup(identifierNode.Identifier);

            return node is FunctionDefinitionAstNode;
        }

        private AstNode ParseFunctionCall(IdentifierAstNode identifierNode)
        {
            var node = (FunctionDefinitionAstNode)_symbolTable.Lookup(identifierNode.Identifier);

            var expressions = new List<AstNode>();

            var arguments = node.Arguments;

            for (var i = 0; i < arguments.Length; i++)
                expressions.Add(ParseExpression());

            return new FunctionCallAstNode(identifierNode.Token, expressions.ToArray());
        }

        private bool IsLocalIdentifier(IdentifierAstNode identifierNode)
        {
            return _symbolTable.IsDefined(GetFullName(identifierNode.Identifier));
        }

        private bool IsGlobalIdentifier(IdentifierAstNode identifierNode)
        {
            return _symbolTable.IsDefined(identifierNode.Identifier);
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
            var node = new IdentifierAstNode(_currentToken, (string)_currentToken.Value);

            Eat(TokenType.Identifier);

            return node;
        }

        private string GetFullName(string name)
        {
            return _currentScope == null ? name : _currentScope + "." + name;
        }

        private AstNode ParseAssignment(AstNode variable)
        {
            var assignmentToken = _currentToken;
            Eat(TokenType.Assignment);

            return new AssignmentAstNode(assignmentToken, variable, ParseExpression());
        }
    }
}
