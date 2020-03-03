using System;
using Codewars.Katas.SimpleInteractiveInterpreter.Ast;
using System.Collections.Generic;
using System.Linq;

namespace Codewars.Katas.SimpleInteractiveInterpreter
{
    public class Evaluator
    {
        private readonly Dictionary<string, double> _variableValues = new Dictionary<string, double>();

        private readonly SymbolTable _symbolTable;

        private readonly Stack<string> _callStack = new Stack<string>();

        public Evaluator(SymbolTable symbolTable)
        {
            _symbolTable = symbolTable;
        }

        public double? Evaluate(AstNode node)
        {
            if (IsEmpty(node) || IsFunctionDefinition(node))
                return null;

            return EvaluateNode(node);
        }

        private static bool IsFunctionDefinition(AstNode node)
        {
            return node is FunctionDefinitionAstNode;
        }

        private static bool IsEmpty(AstNode node)
        {
            return node is EmptyAstNode;
        }

        private double EvaluateNode(AstNode node)
        {
            if (IsDoubleConst(node))
                return EvaluateDoubleConst(node);

            if (IsBinaryOperation(node))
                return EvaluateBinaryOperation((BinaryOperationAstNode)node);

            if (IsAssignment(node))
                return EvaluateAssign((AssignmentAstNode)node);

            if (IsCurrentScopeVariable(node))
                return GetCurrentScopeVariableValue((IdentifierAstNode)node);

            if (IsGlobalScopeVariable(node))
                return GetGlobalScopeVariableValue((IdentifierAstNode)node);

            if (IsFunctionCall(node))
                return EvaluateFunctionCall((FunctionCallAstNode)node);

            throw new InvalidOperationException();
        }

        private bool IsGlobalScopeVariable(AstNode node)
        {
            return node is IdentifierAstNode identifier && _variableValues.ContainsKey(identifier.Name);
        }

        private double GetGlobalScopeVariableValue(IdentifierAstNode node)
        {
            return _variableValues[node.Name];
        }

        private static bool IsFunctionCall(AstNode node)
        {
            return node is FunctionCallAstNode;
        }

        private double EvaluateFunctionCall(FunctionCallAstNode node)
        {
            CreateCallContext(node);

            var value = EvaluateFunctionBody(node.Name);

            RemoveCallContext(_callStack.Pop());

            return value;
        }

        private double EvaluateFunctionBody(string name)
        {
            var definition = GetFunctionDefinition(name);

            return EvaluateNode(definition.Body);
        }

        private void CreateCallContext(FunctionCallAstNode node)
        {
            var functionName = node.Name;

            _callStack.Push(functionName);

            var definition = GetFunctionDefinition(functionName);

            EvaluateArguments(definition.Arguments, node.Arguments);
        }

        private FunctionDefinitionAstNode GetFunctionDefinition(string name)
        {
            return (FunctionDefinitionAstNode)_symbolTable.Lookup(name);
        }

        private void RemoveCallContext(string call)
        {
            var scopeVariables = _variableValues.Keys.Where(t => t.StartsWith(call + ".")).ToArray();

            foreach (var scopeVariable in scopeVariables)
                _variableValues.Remove(scopeVariable);
        }

        private void EvaluateArguments(AstNode[] identifiers, AstNode[] expressions)
        {
            for (var i = 0; i < identifiers.Length; i++)
            {
                var identifier = (IdentifierAstNode)identifiers[i];

                _variableValues.Add(GetFullName(identifier.Name), EvaluateNode(expressions[i]));
            }
        }

        private string GetFullName(string name)
        {
            return _callStack.Count == 0 ? name : _callStack.Peek() + "." + name;
        }

        private bool IsCurrentScopeVariable(AstNode node)
        {
            return node is IdentifierAstNode identifier && _variableValues.ContainsKey(GetFullName(identifier.Name));
        }

        private double GetCurrentScopeVariableValue(IdentifierAstNode node)
        {
            return _variableValues[GetFullName(node.Name)];
        }

        private static bool IsAssignment(AstNode node)
        {
            return node is AssignmentAstNode;
        }

        private double EvaluateAssign(AssignmentAstNode assignment)
        {
            var name = GetFullName(assignment.Variable.Name);

            var value = EvaluateNode(assignment.Expression);

            UpdateOrInsertVariableValue(name, value);

            return value;
        }

        private void UpdateOrInsertVariableValue(string name, double value)
        {
            if (_variableValues.ContainsKey(name))
                _variableValues[name] = value;
            else
                _variableValues.Add(name, value);
        }

        private static bool IsDoubleConst(AstNode node)
        {
            return node is DoubleConstAstNode;
        }

        private static double EvaluateDoubleConst(AstNode node)
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
                throw new InvalidOperationException("Invalid token type");
        }

        private static bool IsBinaryOperation(AstNode node)
        {
            return node is BinaryOperationAstNode;
        }
    }
}
