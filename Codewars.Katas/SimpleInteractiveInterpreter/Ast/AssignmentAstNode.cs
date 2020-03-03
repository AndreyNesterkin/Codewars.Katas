namespace Codewars.Katas.SimpleInteractiveInterpreter.Ast
{
    public class AssignmentAstNode : AstNode
    {
        public AssignmentAstNode(Token token, IdentifierAstNode variable, AstNode expression) : base(token)
        {
            Variable = variable;
            Expression = expression;
        }

        public IdentifierAstNode Variable { get; }

        public AstNode Expression { get; }
    }
}
