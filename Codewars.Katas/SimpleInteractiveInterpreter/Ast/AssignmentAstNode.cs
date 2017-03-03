namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public class AssignmentAstNode : AstNode
    {
        public AssignmentAstNode(Token token, AstNode variable, AstNode expr) : base(token)
        {
            Variable = variable;
            Expression = expr;
        }

        public AstNode Variable { get; private set; }

        public AstNode Expression { get; private set; }
    }
}
