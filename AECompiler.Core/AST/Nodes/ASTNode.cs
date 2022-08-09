using AECompiler.Core.AST.Tokens;
using AECompiler.Core.Interpreters;

namespace AECompiler.Core.AST.Nodes
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
}
