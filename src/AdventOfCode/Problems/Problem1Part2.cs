namespace AdventOfCode.Problems;

internal class Problem1Part2 : IProblem
{
    public static int Id => 1;

    public static int Solve(ReadOnlySpan<byte> input)
    {
        //{
        //    Span<int> topCalories = new int[3];
        //    var calories = 0;

        //    foreach (var ina in input)
        //    {
        //        if (ina.Length == 0)
        //        {
        //            ReplaceIfBigger(calories, topCalories);
        //            calories = 0;
        //        }
        //        else
        //        {
        //            calories += int.Parse(ina);
        //        }
        //    }
        //    var totalCalories = 0;
        //    foreach (var topCalory in topCalories)
        //    {
        //        totalCalories += topCalory;
        //    }
        //    return totalCalories;
        //}

        //static void ReplaceIfBigger(int calories, Span<int> topCalories)
        //{
        //    foreach (ref var value in topCalories)
        //    {
        //        if (calories > value)
        //        {
        //            var oldValue = value;
        //            value = calories;
        //            ReplaceIfBigger(oldValue, topCalories);
        //            break;
        //        }
        //    }
        //    topCalories.Sort();
        //}
        return -1;
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