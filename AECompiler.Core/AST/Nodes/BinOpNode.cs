using AECompiler.Core.AST.Tokens;
using AECompiler.Core.Interpreters;

namespace AECompiler.Core.AST.Nodes
{
    public class BinOpNode : ASTNode
    {
        public BinOpNode(Token token) : base(token)
        {
            _childNodes = new ASTNode[2];
        }

        public BinOpNode(Token token, ASTNode left, ASTNode right) : base(token)
        {
            _childNodes = new ASTNode[2] { left, right };
        }

        public override int AcceptVisitor(Interpreter interpreter)
        {
            return interpreter.Process(this);
        }
    }
}
