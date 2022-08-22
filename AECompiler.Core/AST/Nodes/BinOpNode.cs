using AECompiler.Core.AST.Tokens;
using AECompiler.Core.CodeGeneration.IdGeneration;
using AECompiler.Core.Interpreters;

namespace AECompiler.Core.AST.Nodes
{
    internal sealed class BinOpNode : ASTNode
    {
        public BinOpNode(Token token) : base(token)
        {
            ChildNodes = new ASTNode[2];
        }

        public BinOpNode(Token token, ASTNode left, ASTNode right) : base(token)
        {
            ChildNodes = new[] { left, right };
        }

        public override StoreId AcceptVisitor(IInterpreter basicInterpreter)
        {
            return basicInterpreter.Process(this);
        }
    }
}
