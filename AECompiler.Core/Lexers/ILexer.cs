using AECompiler.Core.AST.Tokens;

namespace AECompiler.Core.Lexers
{
    internal interface ILexer
    {
        public void Tokenize(string text);
        public Token GetNextToken();
        public Token GetLastProcessedToken();
    }
}