namespace Codewars.Katas.SimpleInteractiveInterpreter
{
    public class Token
    {
        public Token(TokenType type, object value)
        {
            Type = type;
            Value = value;
        }

        public Token(TokenType type) : this(type, null)
        {
        }

        public TokenType Type { get; private set; }

        public object Value { get; private set; }
    }
}
