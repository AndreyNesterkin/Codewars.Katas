namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public abstract class AstNode
    {
        protected AstNode(Token token)
        {
            Token = token;
        }

        public Token Token { get; private set; }
    }
}
