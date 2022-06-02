using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionAnalyzer
{
    abstract public class ASTNode
    {
        protected Token _token;
        protected ASTNode[] _childNodes;

        public ASTNode(Token token)
        {
            _token = token;
            _childNodes = null;
        }

        public Token GetToken()
        {
            return _token;
        }

        public bool TryGetChild(int num, out ASTNode node)
        {
            if (_childNodes is null || num >= _childNodes.Length)
            {
                node = null;
                return false;
            }

            node = _childNodes[num];
            return true;
        }

        public bool TrySetChild(int num, in ASTNode node)
        {
            if (_childNodes is null || num >= _childNodes.Length)
            {
                return false;
            }
             
            _childNodes[num] = node;
            return true;
        }

        public abstract int AcceptVisitor(Interpreter interpreter);
    }

    public class BinOpNode : ASTNode
    {
        public BinOpNode(Token token) : base(token)
        {
            _childNodes = new ASTNode[2];
        }

        public BinOpNode(Token token, ASTNode left, ASTNode right) : base(token)
        {
            _childNodes = new ASTNode[2] { left, right };
        }

        public override int AcceptVisitor(Interpreter interpreter)
        {
            return interpreter.Process(this);
        }
    }

    public class IntNode : ASTNode
    {
        private int _value;

        public IntNode(Token token) : base(token)
        {
            _value = (int) _token.Value;
        }

        public int GetValue()
        {
            return _value;
        }

        public override int AcceptVisitor(Interpreter interpreter)
        {
            return interpreter.Process(this);
        }
    }
}
