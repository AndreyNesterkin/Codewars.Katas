namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public class BinaryOperationAstNode : AstNode
    {
        public BinaryOperationAstNode(Token token, AstNode leftOperand, AstNode rightOperand) : base(token)
        {
            LeftOperand = leftOperand;

            RightOperand = rightOperand;
        }

        public AstNode LeftOperand { get; private set; }

        public AstNode RightOperand { get; private set; }
    }
}
