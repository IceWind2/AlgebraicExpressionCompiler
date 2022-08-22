using AECompiler.Core.AST.Nodes;
using AECompiler.Core.AST.Tokens;
using AECompiler.Core.Lexers;
using AECompiler.Core.Parsers;

namespace AECompiler.Tests;

public class ParserTests
{
    [Fact]
    public void Parse_EmptyExpression_ReturnsNoOpNode()
    {
        var parser = CreateDefaultParser();
        const string expression = "";

        ASTNode root = parser.Parse(expression);

        Assert.Null(root.GetToken());
    }

    [Fact]
    public void Parse_Number_ReturnsIntToken()
    {
        var parser = CreateDefaultParser();
        const string expression = "0";

        ASTNode root = parser.Parse(expression);

        Assert.Equal(TokenType.Int, root.GetToken().Type);
    }
    
    [Fact]
    public void Parse_OpSymbol_ThrowsException()
    {
        var parser = CreateDefaultParser();
        const string expression = "+";

       Assert.Throws<ArgumentException>(() => parser.Parse(expression));
    }
    
    [Fact]
    public void Parse_Expression_ReturnsOpToken()
    {
        var parser = CreateDefaultParser();
        const string expression = "0 + 0";

        ASTNode root = parser.Parse(expression);

        Assert.Equal(TokenType.Plus, root.GetToken().Type);
    }
    
    private RecursiveParser CreateDefaultParser()
    {
        var lexer = new LazyLexer();

        return new RecursiveParser(lexer);
    } 
}