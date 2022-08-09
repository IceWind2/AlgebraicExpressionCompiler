using AECompiler.Core.AST.Nodes;

namespace AECompiler.Core.Parsers
{
    internal interface IParser
    {
        public ASTNode Parse(string expression);
    }
}