using AdventOfCode.Tokens;

namespace AdventOfCode.Problems;

internal struct Problem1Part1 : IProblem
{
    public static int Id => 1;
    public static int Part => 1;

    public static ProblemResult Solve(ReadOnlySpan<byte> input)
    {
        var cursor = new Cursor(input);
        var maxCalories = 0;
        var calories = 0;
        var token = cursor.ReadNextToken(true);
        while (token.IsValid)
        {
            if (token.Type == TokenType.Integer)
            {
                calories += token.AsInteger();
            }
            if (token.Type == TokenType.NewLine)
            {
                maxCalories = Math.Max(maxCalories, calories);
                calories = 0;
            }
            token = cursor.ReadNextToken(true);
        }
        return maxCalories;
    }

    public static ProblemResult SolveNaive(ReadOnlySpan<string> input)
    {
        var maxCalories = 0;
        var calories = 0;

        foreach (var ina in input)
        {
            if (ina.Length == 0)
            {
                maxCalories = Math.Max(calories, maxCalories);
                calories = 0;
            }
            else
            {
                calories += int.Parse(ina);
            }
        }
        return maxCalories;
    }
}