namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public class FunctionCallAstNode : AstNode
    {
        public FunctionCallAstNode(Token token, AstNode[] arguments) : base(token)
        {
            Name = (string)token.Value;
            Arguments = arguments;
        }

        public string Name { get; }

        public AstNode[] Arguments { get; }
    }
}
