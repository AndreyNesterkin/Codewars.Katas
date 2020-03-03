namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public class BinaryOperationAstNode : AstNode
    {
        public BinaryOperationAstNode(Token token, AstNode leftOperand, AstNode rightOperand) : base(token)
        {
            LeftOperand = leftOperand;

            RightOperand = rightOperand;
        }

        public AstNode LeftOperand { get; }

        public AstNode RightOperand { get; }

        public override string ToString()
        {
            return $"({LeftOperand} {Token.Value} {RightOperand})";
        }
    }
}
