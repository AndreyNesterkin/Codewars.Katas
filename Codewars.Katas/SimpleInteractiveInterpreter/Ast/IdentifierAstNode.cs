namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public class IdentifierAstNode : AstNode
    {
        public IdentifierAstNode(Token token, string value) : base(token)
        {
            Identifier = value;
        }

        public string Identifier { get; private set; }
    }
}
