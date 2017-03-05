﻿using Codewars.Katas.SimpleInteractiveInterpreter.Ast;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codewars.Katas.SimpleInteractiveInterpreter
{
    public class SymbolTable
    {
        private string[] SystemNames = new[] 
        {
            "fn"
        };

        private Dictionary<string, AstNode> _identifiers = new Dictionary<string, AstNode>();

        public AstNode Lookup(string name)
        {
            return _identifiers[name];
        }

        public void DefineVariable(string name, IdentifierAstNode node)
        {
            name = name.ToLower();

            if (IsSystemName(name))
                throw new InvalidOperationException("'fn' is a system identifier");

            if (HasDefinedFunctionWithSameName(name))
                throw new InvalidOperationException("Function with same name has already defined");

            UpdateOrInsert(name, node);
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
            return SystemNames.Any(t => t == name);
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

        private bool HasDefinedVariableWithSameName(string name)
        {
            return _identifiers.Any(t => t.Key == name &&
                        t.Value.GetType() != typeof(FunctionDefinitionAstNode));
        }
    }
}
