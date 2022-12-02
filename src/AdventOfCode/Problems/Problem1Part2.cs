using AdventOfCode.Tokens;

namespace AdventOfCode.Problems;

internal struct Problem1Part2 : IProblem
{
    public static int Id => 1;
    public static int Part => 1;

    public static int Solve(ReadOnlySpan<byte> input)
    {
        Span<int> topCalories = stackalloc int[3];
        var cursor = new Cursor(input);
        var token = cursor.ReadNextToken(true);
        var calories = 0;
        while (token.IsValid)
        {
            if (token.Type == TokenType.Integer)
            {
                calories += token.AsInteger();
            }
            else if (token.Type == TokenType.NewLine)
            {
                ReplaceIfBigger(calories, topCalories);
                calories = 0;
            }
            token = cursor.ReadNextToken(true);
        }

        // calculate the sum of the top 3
        var totalCalories = 0;
        foreach (var topCalory in topCalories)
        {
            totalCalories += topCalory;
        }
        return totalCalories;

        static void ReplaceIfBigger(int calories, Span<int> topCalories)
        {
            foreach (ref var value in topCalories)
            {
                if (calories > value)
                {
                    // recursive function since the value we replaced might be bigger than some other value
                    var oldValue = value;
                    value = calories;
                    ReplaceIfBigger(oldValue, topCalories);
                    break;
                }
            }
        }
    }

    public static int SolveNaive(ReadOnlySpan<string> input)
    {
        List<int> values = new();
        var calories = 0;
        foreach (var str in input)
        {
            if (str.Length == 0)
            {
                values.Add(calories);
                calories = 0;
            }
            else
            {
                calories += int.Parse(str);
            }
        }

        return values
            .OrderByDescending(i => i)
            .Take(3)
            .Sum();
    }
}