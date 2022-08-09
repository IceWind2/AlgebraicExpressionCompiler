using System;

using AECompiler.Core.AST.Tokens;
using AECompiler.Core.AST.Nodes;
using AECompiler.Core.Lexers;
using AECompiler.Core.Parsers;

namespace AECompiler.Core.Interpreters
{
    public class Interpreter
    {
        private IParser _parser;

        public Interpreter()
        {
            ILexer lexer = new Lexer();
            _parser = new Parser(lexer);
        }

        public int Compile(string expression)
        {
            return Visit(_parser.Parse(expression));
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

            throw new ArgumentException("Interpreter error: Wrong token type");
        }

        internal int Process(IntNode node)
        {
            return node.GetValue();
        }
        
        private int Visit(ASTNode node)
        {
            return node.AcceptVisitor(this);
        }
    }
}
