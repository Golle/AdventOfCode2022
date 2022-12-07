namespace AdventOfCode.Problems;

file enum NodeType
{
    Folder,
    File
}
file class SimpleNode
{
    public string Name { get; set; }
    public uint Size { get; set; }
    public NodeType Type { get; set; }
    public List<SimpleNode> Children { get; set; } = new();
    public long GetTotalSize()
    {
        if (Type == NodeType.File)
        {
            return Size;
        }
        return Size + Children.Sum(c => c.GetTotalSize());
    }

    public void Print(int indentation = 0)
    {
        Console.WriteLine($"{new string(' ', indentation * 2)}{Name} - {Type} ({GetTotalSize()} bytes)");
        foreach (var child in Children)
        {
            child.Print(indentation + 1);
        }
    }

    public IEnumerable<SimpleNode> GetAllFolders()
    {
        if (Type == NodeType.File)
        {
            return Enumerable.Empty<SimpleNode>();
        }

        return Children.SelectMany(c => c.GetAllFolders()).Concat(new[] { this });
    }
}

internal class Problem7Part2 : IProblem
{
    public static int Id => 7;
    public static int Part => 2;
    public static ProblemResult Solve(ReadOnlySpan<byte> input)
    {
        return -1;
    }

    public static ProblemResult SolveNaive(ReadOnlySpan<string> input)
    {
        var root = new SimpleNode { Type = NodeType.Folder, Name = "/" };

        var current = new Stack<SimpleNode>();
        current.Push(root);

        foreach (var ina in input)
        {
            if (ina.StartsWith("$"))
            {
                var splits = ina.Split(" ");
                var command = splits[1];

                switch (command)
                {
                    case "cd":
                        var folder = splits[2];
                        if (folder == "..")
                        {
                            current.Pop();
                        }
                        else if (folder == "/")
                        {
                            current.Clear();
                            current.Push(root);
                        }
                        else
                        {
                            var folderNode = current.Peek().Children.SingleOrDefault(c => c.Name == folder);
                            if (folderNode == null)
                            {
                                folderNode = new SimpleNode { Name = folder, Type = NodeType.Folder };
                                current.Peek().Children.Add(folderNode);
                            }
                            current.Push(folderNode);
                        }
                        break;
                    case "ls":
                        // I think we can ignore this
                        break;

                    default:
                        throw new NotSupportedException("Command not suppported.");
                }
            }
            else if (ina[0] is >= '0' and <= '9')
            {
                var splits = ina.Split(" ");
                current.Peek().Children.Add(new SimpleNode() { Name = splits[1], Type = NodeType.File, Size = uint.Parse(splits[0]) });
            }
            else if (ina.StartsWith("dir"))
            {
                var splits = ina.Split(" ");
                if (!current.Peek().Children.Any(c => c.Type == NodeType.Folder && c.Name == splits[0]))
                {
                    current.Peek().Children.Add(new SimpleNode { Name = splits[0], Type = NodeType.Folder });
                }
            }
        }

        var currentDiskUsage = root.GetTotalSize();
        var freeSpace = 70_000_000 - currentDiskUsage;
        var needed = 30_000_000 - freeSpace;

        var simpleNodes = root.GetAllFolders().ToArray();
        var size = simpleNodes
            .OrderBy(node => node.GetTotalSize())
            .First(node => node.GetTotalSize() > needed)
            .GetTotalSize();


        return (int)size;
    }
}