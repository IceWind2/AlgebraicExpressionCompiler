using AECompiler.Core.AST.Tokens;
using AECompiler.Core.Interpreters;

namespace AECompiler.Core.AST.Nodes
{
    public class IntNode : ASTNode
    {
        private int _value;

        public IntNode(Token token) : base(token)
        {
            _value = (int)_token.Value;
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
