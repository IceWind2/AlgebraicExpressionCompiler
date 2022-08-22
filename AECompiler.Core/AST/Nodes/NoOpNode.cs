using AECompiler.Core.CodeGeneration.IdGeneration;
using AECompiler.Core.Interpreters;

namespace AECompiler.Core.AST.Nodes
{
    internal sealed class NoOpNode : ASTNode
    {
        public NoOpNode() : base(null) {}

        public override StoreId AcceptVisitor(IInterpreter basicInterpreter)
        {
            return basicInterpreter.Process(this);
        }
    }
}