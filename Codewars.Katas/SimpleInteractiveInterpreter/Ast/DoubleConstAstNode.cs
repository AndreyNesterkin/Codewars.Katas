namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public class DoubleConstAstNode : AstNode
    {
        public DoubleConstAstNode(Token token) : base(token) 
        {

        }

        public double Value => (double)Token.Value;

        public override string ToString()
        {
            return $"{Token.Value}";
        }
    }
}
