namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public class DoubleConstAstNode : AstNode
    {
        public DoubleConstAstNode(Token token) : base(token) 
        {

        }

        public double DoubleConst { get { return (double)Token.Value; } }
    }
}
