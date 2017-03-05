﻿namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public class DoubleConstAstNode : AstNode
    {
        public DoubleConstAstNode(Token token) : base(token) 
        {

        }

        public double Value { get { return (double)Token.Value; } }

        public override string ToString()
        {
            return $"{Token.Value}";
        }
    }
}
