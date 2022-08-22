using AECompiler.Core.AST.Tokens;
using AECompiler.Core.Lexers;

namespace AECompiler.Tests;

public class LexerTests
{
    [Fact]
    public void Tokenize_NullExpression_ContainsEmptyString()
    {
        var lexer = new LazyLexer();

        lexer.Tokenize(null);

        Assert.Empty(lexer.Expression);
    }

    [Fact]
    public void Tokenize_StringExpression_ContainsSameString()
    {
        var lexer = new LazyLexer();
        const string expression = "0";

        lexer.Tokenize(expression);

        Assert.Equal(expression, lexer.Expression);
    }

    [Fact]  
    public void GetNextToken_EmptyExpression_ReturnsEOFToken()
    {
        var lexer = new LazyLexer();
        
        Token token = lexer.GetNextToken();

        Assert.Equal(TokenType.Eof, token.Type);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("241  ")]
    [InlineData("  3443")]
    public void GetNextToken_Number_ReturnsSameNumber(string expression)
    {
        var lexer = new LazyLexer(expression);

        Token token = lexer.GetNextToken();

        Assert.Equal(expression.Trim(), token.Value.ToString());
    }

    [Theory]
    [InlineData("+")]
    [InlineData("   *  ")]
    public void GetNextToken_OperationSymbol_ReturnsSameSymbol(string expression)
    {
        var lexer = new LazyLexer(expression);

        Token token = lexer.GetNextToken();

        Assert.Equal(expression.Trim(), token.Value.ToString());
    }

    [Fact]
    public void GetNextToken_InvalidSymbol_ThrowsArgumentException()
    {
        const string expression = "$#@";
        var lexer = new LazyLexer(expression);

        Assert.Throws<ArgumentException>(()=>lexer.GetNextToken());
    }

    [Fact]
    public void GetNextToken_SeveralTokens_ReturnsSecondToken()
    {
        const string expression = "232 + 55";
        var lexer = new LazyLexer(expression);

        lexer.GetNextToken();
        Token token = lexer.GetNextToken();

        Assert.Equal("+", token.Value.ToString());
    }

    [Fact]
    public void GetLastProcessedToken_EmptyExpression_ReturnsNull()
    {
        var lexer = new LazyLexer();

        Token token = lexer.GetLastProcessedToken();

        Assert.Null(token);
    }

    [Fact]
    public void GetLastProcessedToken_Token_ReturnsSameToken()
    {
        const string expression = "232";
        var lexer = new LazyLexer(expression);

        lexer.GetNextToken();
        Token token = lexer.GetLastProcessedToken();

        Assert.Equal(expression, token.Value.ToString());
    }
}