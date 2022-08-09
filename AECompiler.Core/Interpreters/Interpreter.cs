using System;

using AECompiler.Core.AST.Tokens;
using AECompiler.Core.AST.Nodes;

namespace AECompiler.Core.Interpreters
{
    public class Interpreter
    {
        public Interpreter() { }

        public int Visit(ASTNode node)
        {
            return node.AcceptVisitor(this);
        }

        internal int Process(BinOpNode node)
        {
            ASTNode child1, child2;
            node.TryGetChild(0, out child1);
            node.TryGetChild(1, out child2);

            switch (node.GetToken().Type)
            {
                case TokenType.Plus:
                    return Visit(child1) + Visit(child2);

                case TokenType.Minus:
                    return Visit(child1) - Visit(child2);

                case TokenType.Mul:
                    return Visit(child1) * Visit(child2);

                case TokenType.Div:
                    return Visit(child1) / Visit(child2);
            }

            throw new Exception("Interpreter error: Wrong token type");
        }

        internal int Process(IntNode node)
        {
            return node.GetValue();
        }
    }
}
