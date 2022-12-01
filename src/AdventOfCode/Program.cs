using AdventOfCode;

// Problem  1
{
    var input = ReadInput(1);
    var result = Problem1.Solve(input);
    Console.WriteLine($"Result for problem1: {result}");
}

static string[] ReadInput(int problemNumber)
{
    const string basePath = @"..\..\..\..\..\inputs";

    var path = Path.GetFullPath(Path.Combine(basePath, $"problem_{problemNumber}.txt"));
    Console.WriteLine($"Reading input from {path}");
    if (!File.Exists(path))
    {
        Console.Error.WriteLine($"File {path} does not exist.");
        throw new InvalidOperationException("File does not exist.");
    }
    return File.ReadAllLines(path);

}