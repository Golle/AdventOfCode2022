namespace AdventOfCode.Tokens;

internal ref struct Cursor
{
    private byte INVALID = 0;
    private readonly ReadOnlySpan<byte> _input;
    private int _current;

    private byte Current => _current < _input.Length ? _input[_current] : (byte)0;
    public Cursor(ReadOnlySpan<byte> input)
        => _input = input;

    public Token ReadNextToken(bool ignoreNewLineAfterToken)
    {
        do
        {
            switch ((char)Current)
            {
                case >= '0' and <= '9':
                    var token = NumberLiteral();
                    if (ignoreNewLineAfterToken)
                    {
                        NewlineLiteral();
                    }
                    return token;
                case '\n' or '\r':
                    return NewlineLiteral();
                case ' ':
                    Advance();
                    break;
            }
        } while (Current != INVALID);
        return Token.Invalid;
    }


    private bool Advance(int count = 1)
        => (_current += count) <= _input.Length;

    private byte Peek(int count = 1)
    {
        var index = _current + count;
        if (index >= _input.Length)
        {
            return INVALID;
        }
        return _input[index];
    }

    private Token NumberLiteral()
    {
        static bool IsNumber(char c) => c is >= '0' and <= '9';
        var index = _current;
        var count = 0;
        while (IsNumber((char)Current))
        {
            Advance();
            count++;
        }
        return new Token
        {
            Data = _input.Slice(index, count),
            Type = TokenType.Integer
        };
    }

    private Token NewlineLiteral()
    {
        if (Current == '\r')
        {
            Advance();
            if (Current == '\n')
            {
                Advance();
            }
        }
        return new Token { Type = TokenType.NewLine };
    }
}