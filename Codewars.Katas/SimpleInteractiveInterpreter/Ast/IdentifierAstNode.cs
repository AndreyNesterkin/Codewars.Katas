namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public class IdentifierAstNode : AstNode
    {
        public IdentifierAstNode(Token token) : base(token)
        {
            Name = (string)token.Value;
        }

        public string Name { get; }
    }
}
