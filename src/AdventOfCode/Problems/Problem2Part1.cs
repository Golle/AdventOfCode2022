namespace AdventOfCode.Problems;

// Rock A, X 2 point
// Paper B, Y 1 point
// Scissor C, Z 3 point

file enum PlayerChoice
{
    Rock = 1,
    Paper = 2,
    Scissor = 3
}
internal class Problem2Part1 : IProblem
{
    public static int Id => 2;

    public static int Solve(ReadOnlySpan<byte> input)
    {
        return -3;
    }

    public static int SolveNaive(ReadOnlySpan<string> input)
    {
        var myScore = 0;
        
        foreach (var ina in input)
        {
            var values = ina.Split(" ", 2);
            var opponent = values[0][0] switch
            {
                'A' => PlayerChoice.Rock,
                'B' => PlayerChoice.Paper,
                'C' => PlayerChoice.Scissor,
            };
            var me = values[1][0] switch
            {
                'X' => PlayerChoice.Rock,
                'Y' => PlayerChoice.Paper,
                'Z' => PlayerChoice.Scissor,
            };

            // Tie
            if (opponent == me)
            {
                myScore += (int)me + 3;
            }
            // Win
            else if (
                (opponent is PlayerChoice.Paper && me is PlayerChoice.Scissor) ||
                (opponent is PlayerChoice.Scissor && me is PlayerChoice.Rock) ||
                (opponent is PlayerChoice.Rock && me is PlayerChoice.Paper)
                )
            {
                myScore += (int)me + 6;
            }
            else
            {
                myScore += (int)me;
            }
        }
        return myScore;
    }
}