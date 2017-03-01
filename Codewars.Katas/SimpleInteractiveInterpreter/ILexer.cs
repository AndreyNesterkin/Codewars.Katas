namespace Codewars.Katas.SimpleInteractiveInterpreter
{
    public interface ILexer
    {
        void SetText(string text);

        Token ReadNextToken();
    }
}
