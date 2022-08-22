using System;

using AECompiler.Core.AST.Nodes;
using AECompiler.Core.AST.Tokens;
using AECompiler.Core.Lexers;

namespace AECompiler.Core.Parsers
{
    /* ---------------- Grammar ----------------
       expr: (minus) term ((plus | minus) term)*
       term: factor ((mul | div) factor)*
       factor: INT | Lpar expr Rpar
       ----------------------------------------- */

    internal sealed class RecursiveParser : IParser
    {
        private readonly ILexer _lexer;
        private Token _currentToken;

        public RecursiveParser()
        {
            _lexer = new LazyLexer();
        }
        
        public RecursiveParser(ILexer lexer)
        {
            _lexer = lexer;
        }

        // Returns the root of the generated AST
        public ASTNode Parse(string expression)
        {
            _lexer.Tokenize(expression);
            _currentToken = _lexer.GetNextToken();

            if (_currentToken.Type == TokenType.Eof)
            {
                return new NoOpNode();
            }
            
            ASTNode res = _expr();

            if (_currentToken.Type != TokenType.Eof)
            {
                _throwError();
            }

            return res;
        }
        
        private ASTNode _expr()
        {
            /*int neg = 1;

            if (_currentToken.Type == TokenType.Minus)
            {
                _consumeToken(TokenType.Minus);
                neg = -1;
            }*/

            ASTNode root = _term();
            BinOpNode binOp;

            while (_currentToken.Type == TokenType.Plus || _currentToken.Type == TokenType.Minus)
            {
                binOp = new BinOpNode(_currentToken);
                _consumeToken(_currentToken.Type);
                binOp.SetChild(0, root);
                binOp.SetChild(1, _term());

                root = binOp;
            }

            return root;
        }

        private ASTNode _term()
        {
            ASTNode root = _factor();
            BinOpNode binOp;

            while (_currentToken.Type == TokenType.Mul || _currentToken.Type == TokenType.Div)
            {
                binOp = new BinOpNode(_currentToken);
                _consumeToken(_currentToken.Type);
                binOp.SetChild(0, root);
                binOp.SetChild(1, _factor());

                root = binOp;
            }

            return root;
        }

        private ASTNode _factor()
        {
            ASTNode res;

            switch (_currentToken.Type)
            {
                case TokenType.Int:
                    res = new IntNode(_currentToken);
                    _consumeToken(TokenType.Int);
                    return res;
                case TokenType.Lpar:
                    _consumeToken(TokenType.Lpar);
                    res = _expr();
                    _consumeToken(TokenType.Rpar);
                    return res;
                default:
                    _throwError();
                    return null;
            }
        }
        
        private void _consumeToken(TokenType type)
        {
            if (_currentToken.Type != type)
            {
                _throwError();
            }
            else
            {
                _currentToken = _lexer.GetNextToken();
            }
        }
        
        private void _throwError()
        {
            throw new ArgumentException("Parsing error: incorrect expression");
        }
    }
}
