namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public class FunctionCallAstNode : AstNode
    {
        public FunctionCallAstNode(Token token, AstNode[] arguments) : base(token)
        {
            Name = (string)token.Value;
            Arguments = arguments;
        }

        public string Name { get; private set; }

        public AstNode[] Arguments { get; private set; }
    }
}
