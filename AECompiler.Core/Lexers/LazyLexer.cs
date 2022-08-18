using System;
using System.Collections.Generic;
using System.Linq;

using AECompiler.Core.AST.Tokens;

namespace AECompiler.Core.Lexers
{
    internal sealed class LazyLexer : ILexer
    {
        private readonly List<Token> _tokens;
        private int _currentPos;
        private char _currentChar;

        public LazyLexer()
        {
            Expression = "";
            _tokens = new List<Token>();
            _currentPos = -1;
            _currentChar = '\0';
        }
        
        public LazyLexer(string expression) : this()
        {
            Tokenize(expression);
        }

        public void Tokenize(string expression)
        {
            _tokens.Clear();

            if (string.IsNullOrEmpty(expression))
            {
                Expression = "";
                _currentChar = '\0';
                _currentPos = -1;
            }
            else
            {
                Expression = expression;
                _currentChar = Expression[0];
                _currentPos = 0;
            }
        }
        
        public Token GetNextToken()
        {
            while (_currentChar != '\0')
            {
                while (char.IsWhiteSpace(_currentChar))
                {
                    _nextChar();
                }

                if (char.IsDigit(_currentChar))
                {
                    _tokens.Add(new Token(TokenType.Int, _buildNumber(), _currentPos));
                    return _tokens.Last();
                }

                if (_currentChar == '+')
                {
                    _tokens.Add(new Token(TokenType.Plus, '+', _currentPos));
                    _nextChar();
                    return _tokens.Last();
                }

                if (_currentChar == '-')
                {
                    _tokens.Add(new Token(TokenType.Minus, '-', _currentPos));
                    _nextChar();
                    return _tokens.Last();
                }

                if (_currentChar == '*')
                {
                    _tokens.Add(new Token(TokenType.Mul, '*', _currentPos));
                    _nextChar();
                    return _tokens.Last();
                }

                if (_currentChar == '/')
                {
                    _tokens.Add(new Token(TokenType.Div, '/', _currentPos));
                    _nextChar();
                    return _tokens.Last();
                }

                if (_currentChar == '(')
                {
                    _tokens.Add(new Token(TokenType.Lpar, '(', _currentPos));
                    _nextChar();
                    return _tokens.Last();
                }

                if (_currentChar == ')')
                {
                    _tokens.Add(new Token(TokenType.Rpar, ')', _currentPos));
                    _nextChar();
                    return _tokens.Last();
                }

                throw new ArgumentException("Tokenization error: Invalid symbol");
            }

            return new Token(TokenType.Eof, null);
        }
        
        public Token GetLastProcessedToken()
        {
            return _tokens.Count != 0 ? _tokens.Last() : Token.Empty;
        }
        
        public string Expression { get; private set; }
        
        private void _nextChar()
        {
            _currentPos += 1;

            _currentChar = _currentPos >= Expression.Length ? '\0' : Expression[_currentPos];
        }

        private int _buildNumber()
        {
            var number = new System.Text.StringBuilder();

            while (char.IsDigit(_currentChar))
            {
                number.Append(_currentChar);
                _nextChar();
            }

            return int.Parse(number.ToString());
        }
    }
}
