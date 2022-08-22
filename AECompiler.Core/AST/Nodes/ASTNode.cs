using System;

using AECompiler.Core.AST.Tokens;
using AECompiler.Core.CodeGeneration.IdGeneration;
using AECompiler.Core.Interpreters;

namespace AECompiler.Core.AST.Nodes
{
    internal abstract class ASTNode
    {
        protected readonly Token Token;
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

        public ASTNode GetChild(int idx)
        {
            if (ChildNodes is null || idx >= ChildNodes.Length)
            {
                throw new InvalidOperationException("ASTNode: child not found");
            }

            return ChildNodes[idx];
        }

        public void SetChild(int idx, in ASTNode node)
        {
            if (ChildNodes is null || idx >= ChildNodes.Length)
            {
                throw new InvalidOperationException("ASTNode: child not found");
            }

            ChildNodes[idx] = node;
        }

        public abstract StoreId AcceptVisitor(IInterpreter basicInterpreter);
    }
}
