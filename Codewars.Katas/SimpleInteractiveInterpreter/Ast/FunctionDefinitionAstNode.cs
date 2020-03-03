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

        public string Name { get; }

        public AstNode[] Arguments { get; }

        public AstNode Body { get; }
    }
}
