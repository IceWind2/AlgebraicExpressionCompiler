namespace AECompiler.Core.AST.Tokens
{
    public class Token
    {
        public TokenType Type;
        public object Value;
        public int Pos;

        public Token(TokenType type, object value, int pos = -1)
        {
            Type = type;
            Value = value;
            Pos = pos;
        }

        public override string ToString()
        {
            return $"({Type}, {Value})";
        }
    }
}
