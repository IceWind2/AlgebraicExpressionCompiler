using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionAnalyzer
{
    /* ------------- Grammar ---------------
       expr: term ((mul | div) term)*
       term: factor ((plu | min) factor)*
       factor: INT
       ------------------------------------- */

    public class Parser
    {
        private Lexer _lexer;
        private Token _currentToken;

        private void _consumeToken(TokenType type)
        {
            if (_currentToken.Type != type)
            {
                throw new Exception("Parsing error: incorrect expression");
            }
            else
            {
                _currentToken = _lexer.GetNextToken();
            }
        }

        private int _expr()
        {
            int res = _term();

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
            var val = _currentToken.Value;
            _consumeToken(TokenType.Int);
            return (int) val;
        }

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            _currentToken = _lexer.GetNextToken();
        }

        public int Parse()
        {
            return _expr();
        }
    }
}
