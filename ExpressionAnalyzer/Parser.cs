using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionAnalyzer
{
    /* ---------------- Grammar ----------------
       expr: (minus) term ((plus | minus) term)*
       term: factor ((mul | div) factor)*
       factor: INT | Lpar expr Rpar
       ----------------------------------------- */

    public class Parser
    {
        private Lexer _lexer;
        private Token _currentToken;

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            _currentToken = _lexer.GetNextToken();
        }

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

        private ASTNode _expr()
        {
            /*int neg = 1;

            if (_currentToken.Type == TokenType.Minus)
            {
                _consumeToken(TokenType.Minus);
                neg = -1;
            }*/

            ASTNode term1 = _term();
            BinOpNode BinOp;

            while (_currentToken.Type == TokenType.Plus || _currentToken.Type == TokenType.Minus)
            {
                BinOp = new BinOpNode(_currentToken);
                _consumeToken(_currentToken.Type);
                BinOp.TrySetChild(0, term1);
                BinOp.TrySetChild(1, _term());

                term1 = BinOp;
            }

            return term1;
        }

        private ASTNode _term()
        {
            ASTNode fact1 = _factor();
            BinOpNode BinOp;

            while (_currentToken.Type == TokenType.Mul || _currentToken.Type == TokenType.Div)
            {
                BinOp = new BinOpNode(_currentToken);
                _consumeToken(_currentToken.Type);
                BinOp.TrySetChild(0, fact1);
                BinOp.TrySetChild(1, _factor());

                fact1 = BinOp;
            }

            return fact1;
        }

        private ASTNode _factor()
        {
            ASTNode res;

            if (_currentToken.Type == TokenType.Int)
            {
                res = new IntNode(_currentToken);
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
            return null;
        }

        // Returns the root of the generated AST
        public ASTNode Parse()
        {
            ASTNode res = _expr();
            
            if (_currentToken.Type != TokenType.EOF)
            {
                _throwError();
            }

            return res;
        }
    }
}
