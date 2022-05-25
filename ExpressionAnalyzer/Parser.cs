using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionAnalyzer
{
    /* ------------- Grammar ---------------
       expr: (minus) term ((plus | minus) term)*
       term: factor ((mul | div) factor)*
       factor: INT | Lpar expr Rpar
       ------------------------------------- */

    public class Parser
    {
        private Lexer _lexer;
        private Token _currentToken;

        private void _throwError()
        {
            throw new Exception("Parsing error: incorrect expression");
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

        private int _expr()
        {
            int neg = 1;

            if (_currentToken.Type == TokenType.Minus)
            {
                _consumeToken(TokenType.Minus);
                neg = -1;
            }

            int res = _term() * neg;

            while (_currentToken.Type == TokenType.Plus || _currentToken.Type == TokenType.Minus)
            {
                if (_currentToken.Type == TokenType.Plus)
                {
                    _consumeToken(TokenType.Plus);
                    res += _term();
                }

                if (_currentToken.Type == TokenType.Minus)
                {
                    _consumeToken(TokenType.Minus);
                    res -= _term();
                }
            }

            return res;
        }

        private int _term()
        {
            int res = _factor();

            while (_currentToken.Type == TokenType.Mul || _currentToken.Type == TokenType.Div)
            {
                if (_currentToken.Type == TokenType.Mul)
                {
                    _consumeToken(TokenType.Mul);
                    res *= _factor();
                }

                if (_currentToken.Type == TokenType.Div)
                {
                    _consumeToken(TokenType.Div);
                    res /= _factor();
                }
            }

            return res;
        }

        private int _factor()
        {
            int res;

            if (_currentToken.Type == TokenType.Int)
            {
                res = (int) _currentToken.Value;
                _consumeToken(TokenType.Int);
                return res;
            }
            if (_currentToken.Type == TokenType.Lpar)
            {
                _consumeToken(TokenType.Lpar);
                res = _expr();
                _consumeToken(TokenType.Rpar);
                return res;
            }

            _throwError();
            return 0;
        }

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            _currentToken = _lexer.GetNextToken();
        }

        public int Parse()
        {
            int res = _expr();
            
            if (_currentToken.Type != TokenType.EOF)
            {
                _throwError();
            }

            return res;
        }
    }
}
