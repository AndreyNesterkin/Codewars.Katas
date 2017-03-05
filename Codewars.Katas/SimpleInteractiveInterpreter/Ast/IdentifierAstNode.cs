namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public class IdentifierAstNode : AstNode
    {
        public IdentifierAstNode(Token token, string value) : base(token)
        {
            Name = value;
        }

        public string Name { get; private set; }
    }
}
