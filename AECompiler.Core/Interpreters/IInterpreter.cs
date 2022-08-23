using AECompiler.Core.AST.Nodes;
using AECompiler.Core.CodeGeneration.IdGeneration;

namespace AECompiler.Core.Interpreters
{
    internal interface IInterpreter
    {
        public void Process(ASTNode node);
        
        public StoreId Process(BinOpNode node);

        public StoreId Process(IntNode node);

        public StoreId Process(NoOpNode node);
    }
}