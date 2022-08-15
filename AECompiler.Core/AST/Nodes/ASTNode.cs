using AECompiler.Core.AST.Tokens;
using AECompiler.Core.Interpreters;
using AECompiler.Core.Interpreters.IdGeneration;

namespace AECompiler.Core.AST.Nodes
{
    internal abstract class ASTNode
    {
        protected Token Token;
        protected ASTNode[] ChildNodes;

        protected ASTNode(Token token)
        {
            Token = token;
            ChildNodes = null;
        }

        public Token GetToken()
        {
            return Token;
        }

        public bool TryGetChild(int num, out ASTNode node)
        {
            if (ChildNodes is null || num >= ChildNodes.Length)
            {
                node = null;
                return false;
            }

            node = ChildNodes[num];
            return true;
        }

        public bool TrySetChild(int num, in ASTNode node)
        {
            if (ChildNodes is null || num >= ChildNodes.Length)
            {
                return false;
            }

            ChildNodes[num] = node;
            return true;
        }

        public abstract StoreId AcceptVisitor(Interpreter interpreter);
    }
}
