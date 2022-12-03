namespace AdventOfCode.Tokens;

internal ref struct Cursor
{
    private byte INVALID = 0;
    private readonly ReadOnlySpan<byte> _input;
    private int _current;

    private byte Current => _current < _input.Length ? _input[_current] : (byte)0;
    public bool HasMore => _current < _input.Length;
    public Cursor(ReadOnlySpan<byte> input)
        => _input = input;

    public Token ReadNextToken(bool ignoreNewLineAfterToken = false)
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
                case >= 'A' and <= 'Z' or >= 'a' and <= 'z':
                    var stringToken = StringOrCharacterLiteral();
                    if (ignoreNewLineAfterToken)
                    {
                        NewlineLiteral();
                    }
                    return stringToken;
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

    private Token StringOrCharacterLiteral()
    {
        if ((char)Peek() is ' ' or '\n' or '\r' or '\t')
        {
            var index = _current;
            Advance();
            return new Token
            {
                Type = TokenType.Character,
                Data = _input.Slice(index, 1)
            };
        }

        throw new NotSupportedException("Non single character literals are not implemented yet.");
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
        else if (Current == '\n')
        {
            Advance();
            if (Current == '\r')
            {
                Advance();
            }
        }
        return new Token { Type = TokenType.NewLine };
    }
}