namespace AdventOfCode.Tokens;

internal ref struct Token
{
    public TokenType Type;
    public ReadOnlySpan<byte> Data;
    public static Token Invalid => new() { Type = TokenType.Invalid };
    public bool IsValid => Type != TokenType.Invalid;

    public int AsInteger()
    {
        var value = 0;
        foreach (var val in Data)
        {
            value *= 10;
            value += val - '0';
        }
        return value;
    }

    public char AsCharacter() 
        => (char)Data[0];
}