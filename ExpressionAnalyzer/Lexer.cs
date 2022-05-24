using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionAnalyzer
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

    public enum TokenType
    {
        Int,
        Plus,
        Minus,
        Mul,
        Div,
        EOF
    }

    public class Lexer
    {
        private readonly String _text;
        private List<Token> _tokens;
        private int _currentPos;
        private char _currentChar;

        private void _nextChar()
        {
            _currentPos += 1;

            if (_currentPos >= _text.Length)
            {
                _currentChar = '\0';
            }
            else
            {
                _currentChar = _text[_currentPos];
            }
        }

        private int _buildNumber()
        {
            var number = new System.Text.StringBuilder();

            while (Char.IsDigit(_currentChar))
            {
                number.Append(_currentChar);
                _nextChar();
            }

            return Int32.Parse(number.ToString());
        }

        public Lexer (String text)
        {
            _text = text;
            _tokens = new List<Token>();
            _currentPos = 0;
            _currentChar = _text[0];
        }

        public Token GetNextToken()
        {
            while (_currentChar != '\0')
            {
                while (Char.IsWhiteSpace(_currentChar))
                {
                    _nextChar();
                }

                if (Char.IsDigit(_currentChar))
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

                throw new Exception("Tokenization error: Invalid symbol");
            }

            return new Token(TokenType.EOF, null);
        }

        public Token GetLastToken()
        {
            return _tokens.Last();
        }
    }
}
