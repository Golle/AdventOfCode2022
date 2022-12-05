namespace AdventOfCode.Problems;

internal struct Problem5Part2 : IProblem
{
    public static int Id => 5;
    public static int Part => 2;
    public static int Solve(ReadOnlySpan<byte> input)
    {
        return -1;
    }

    public static int SolveNaive(ReadOnlySpan<string> input)
    {
        List<Stack<char>> stacks = new();
        List<List<Problem5Item>> allItems = new();

        var index = 0;
        for (index = 0; index < input.Length; ++index)
        {
            if (TryReadLine(input[index], out var items))
            {
                allItems.Add(items);
            }
            else
            {
                break;
            }

        }

        var numberOfStacks = allItems.SelectMany(a => a.Select(b => b.Stack)).Max();
        for (var i = 0; i < numberOfStacks; ++i)
        {
            stacks.Add(new Stack<char>());
        }

        allItems.Reverse();
        foreach (var allItem in allItems)
        {
            foreach (var item in allItem)
            {
                (stacks[item.Stack - 1] ??= new Stack<char>()).Push(item.Value);
            }
        }
        index += 2;
        for (; index < input.Length; ++index)
        {
            var values = input[index].Split(" ");
            var count = int.Parse(values[1]);
            var from = int.Parse(values[3]) - 1;
            var to = int.Parse(values[5]) - 1;

            foreach (var c in stacks[from].Take(count).Reverse())
            {
                stacks[to].Push(c);
            }
            for (var i = 0; i < count; ++i)
            {
                stacks[from].Pop();
            }
        }

        var result = string.Join("", stacks.Select(s => s.Peek()));
        Console.WriteLine("RESULT: "+result);
        return -1;
    }

    private static bool TryReadLine(string ina, out List<Problem5Item> items)
    {
        items = new();
        var stack = 1;
        for (var i = 0; i < ina.Length; i++)
        {
            var character = ina[i];
            if (character == ' ')
            {
                if (ina[i + 1] is >= '0' and <= '9')
                {
                    return false;
                }

                if (ina[i + 1] is ' ')
                {
                    i += 3;
                    stack++;
                }

                continue;
            }

            // block
            if (character is '[' or ']')
            {
                continue;
            }

            items.Add(new Problem5Item { Value = character, Stack = stack });
            stack++;
        }

        return true;
    }
}