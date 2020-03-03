using Codewars.Katas.SimpleInteractiveInterpreter.Ast;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codewars.Katas.SimpleInteractiveInterpreter
{
    public class SymbolTable
    {
        private readonly string[] _systemNames = {
            "fn"
        };

        private readonly Dictionary<string, AstNode> _identifiers = new Dictionary<string, AstNode>();

        public AstNode Lookup(string name)
        {
            return _identifiers[name];
        }

        public void DefineVariable(string scope, string name, IdentifierAstNode node)
        {
            name = name.ToLower();

            if (IsSystemName(name))
                throw new InvalidOperationException("'fn' is a system identifier");

            if (HasDefinedFunctionWithSameName(name))
                throw new InvalidOperationException("Function with same name has already defined");

            UpdateOrInsert(GetFullName(scope, name), node);
        }

        private static string GetFullName(string scope, string name)
        {
            return string.IsNullOrEmpty(scope) ? name : scope.ToLower() + "." + name;
        }

        private void UpdateOrInsert(string name, AstNode node)
        {
            if (IsDefined(name))
                _identifiers[name] = node;
            else
                _identifiers.Add(name, node);
        }

        private bool IsSystemName(string name)
        {
            return _systemNames.Any(t => t == name);
        }

        private bool HasDefinedFunctionWithSameName(string name)
        {
            return _identifiers.Any(t => t.Key == name && 
                        t.Value.GetType() != typeof(IdentifierAstNode));
        }

        public void DefineFunction(string name, FunctionDefinitionAstNode node)
        {
            name = name.ToLower();

            if (IsSystemName(name))
                throw new InvalidOperationException("'fn' is a system identifier");

            if (HasDefinedVariableWithSameName(name))
                throw new InvalidOperationException("Variable with same name has already defined");

            UpdateOrInsert(name, node);
        }

        public bool IsDefined(string name)
        {
            return _identifiers.ContainsKey(name);
        }

        public bool IsDefined(string scope, string name)
        {
            return IsDefined(GetFullName(scope, name));
        }

        private bool HasDefinedVariableWithSameName(string name)
        {
            return _identifiers.Any(t => t.Key == name &&
                        t.Value.GetType() != typeof(FunctionDefinitionAstNode));
        }
    }
}
