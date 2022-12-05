using AdventOfCode.Tokens;

namespace AdventOfCode.Problems;

// Rock A, 2 point
// Paper B, 1 point
// Scissor C, 3 point


// X Lose
// Y Draw
// Z Win
file enum PlayerChoice
{
    Rock = 1,
    Paper = 2,
    Scissor = 3
}
internal struct Problem2Part2 : IProblem
{
    public static int Id => 2;
    public static int Part => 2;
    public static ProblemResult Solve(ReadOnlySpan<byte> input)
    {
        var cursor = new Cursor(input);

        var myScore = 0;
        do
        {
            var opponent = cursor.ReadNextToken().AsCharacter() switch
            {
                'A' => PlayerChoice.Rock,
                'B' => PlayerChoice.Paper,
                'C' => PlayerChoice.Scissor,
            };
            var me = cursor.ReadNextToken(true).AsCharacter() switch
            {
                'X' => opponent switch
                {
                    PlayerChoice.Paper => PlayerChoice.Rock,
                    PlayerChoice.Rock => PlayerChoice.Scissor,
                    PlayerChoice.Scissor => PlayerChoice.Paper
                },
                'Y' => opponent,
                'Z' => opponent switch
                {
                    PlayerChoice.Paper => PlayerChoice.Scissor,
                    PlayerChoice.Rock => PlayerChoice.Paper,
                    PlayerChoice.Scissor => PlayerChoice.Rock
                }
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


        } while (cursor.HasMore);

        return myScore;
    }

    public static ProblemResult SolveNaive(ReadOnlySpan<string> input)
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
                'X' => opponent switch
                {
                    PlayerChoice.Paper => PlayerChoice.Rock,
                    PlayerChoice.Rock => PlayerChoice.Scissor,
                    PlayerChoice.Scissor => PlayerChoice.Paper
                },
                'Y' => opponent,
                'Z' => opponent switch
                {
                    PlayerChoice.Paper => PlayerChoice.Scissor,
                    PlayerChoice.Rock => PlayerChoice.Paper,
                    PlayerChoice.Scissor => PlayerChoice.Rock
                }
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