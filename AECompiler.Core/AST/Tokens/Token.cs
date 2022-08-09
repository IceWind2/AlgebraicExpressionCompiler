namespace AECompiler.Core.AST.Tokens
{
    internal readonly struct Token
    {
        public static readonly Token Empty = new Token(TokenType.Empty, null);
        
        public readonly TokenType Type;
        public readonly object Value;
        public readonly int Pos;

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
