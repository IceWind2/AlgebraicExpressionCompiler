using AECompiler.Core.AST.Tokens;
using AECompiler.Core.CodeGeneration.IdGeneration;
using AECompiler.Core.Interpreters;

namespace AECompiler.Core.AST.Nodes
{
    internal sealed class IntNode : ASTNode
    {
        private readonly int _value;

        public IntNode(Token token) : base(token)
        {
            _value = (int)Token.Value;
        }

        public int GetValue()
        {
            return _value;
        }

        public override StoreId AcceptVisitor(Interpreter interpreter)
        {
            return interpreter.Process(this);
        }
    }
}
