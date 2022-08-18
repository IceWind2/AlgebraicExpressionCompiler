using AECompiler.Core.AST.Tokens;
using AECompiler.Core.CodeGeneration.IdGeneration;
using AECompiler.Core.Interpreters;

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