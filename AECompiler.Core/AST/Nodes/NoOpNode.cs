using AECompiler.Core.AST.Tokens;
using AECompiler.Core.Interpreters;
using AECompiler.Core.Interpreters.IdGeneration;

namespace AECompiler.Core.AST.Nodes
{
    internal sealed class NoOpNode : ASTNode
    {
        public NoOpNode() : base(Token.Empty) {}

        public override StoreId AcceptVisitor(Interpreter interpreter)
        {
            return interpreter.Process(this);
        }
    }
}