﻿namespace AdventOfCode;

internal class Problem2 : IProblem
{
    public static int Id => 2;

    public static int Solve(ReadOnlySpan<byte> input)
    {
        return -3;
    }

    public static int SolveNaive(ReadOnlySpan<string> input)
    {
        return -1;
    }
}

internal class Problem1 : IProblem
{
    public static int Id => 1;

    public static int Solve(ReadOnlySpan<byte> input)
    {

        return -1;
    }

    public static int SolveNaive(ReadOnlySpan<string> input)
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