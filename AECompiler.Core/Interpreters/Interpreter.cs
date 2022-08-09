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
            node.TryGetChild(0, out var child1);
            node.TryGetChild(1, out var child2);

            return node.GetToken().Type switch
            {
                TokenType.Plus => Visit(child1) + Visit(child2),
                TokenType.Minus => Visit(child1) - Visit(child2),
                TokenType.Mul => Visit(child1) * Visit(child2),
                TokenType.Div => Visit(child1) / Visit(child2),
                _ => throw new ArgumentException("Interpreter error: Wrong token type")
            };
        }

        internal int Process(IntNode node)
        {
            return node.GetValue();
        }

        internal int Process(NoOpNode node)
        {
            return 0;
        }
        
        private int Visit(ASTNode node)
        {
            return node.AcceptVisitor(this);
        }
    }
}
