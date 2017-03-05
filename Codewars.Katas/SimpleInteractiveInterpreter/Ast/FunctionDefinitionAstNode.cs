using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public class FunctionDefinitionAstNode : AstNode
    {
        public FunctionDefinitionAstNode(Token token, AstNode[] arguments, AstNode body) : base(token)
        {
            Name = (string)token.Value;
            Arguments = arguments;
            Body = body;
        }

        public string Name { get; private set; }

        public AstNode[] Arguments { get; private set; }

        public AstNode Body { get; private set; }
    }
}
