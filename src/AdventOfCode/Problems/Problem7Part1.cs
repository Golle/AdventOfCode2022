using System.Runtime.Versioning;

namespace AdventOfCode.Problems;

file enum NodeType
{
    Folder,
    File,
    Root
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
}

internal struct Problem7Part1 : IProblem
{
    public static int Id => 7;
    public static int Part => 1;
    public static ProblemResult Solve(ReadOnlySpan<byte> input)
    {
        return -1;
    }

    public static ProblemResult SolveNaive(ReadOnlySpan<string> input)
    {
        var root = new SimpleNode { Type = NodeType.Root };

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

        //root.Print();
        return (int)GetTheProblemSize(root);

        static long GetTheProblemSize(SimpleNode node)
        {
            const uint MaxSize = 100_000;

            if (node.Type is NodeType.File)
            {
                return 0;
            }

            var size = node.GetTotalSize();
            if (size > MaxSize)
            {
                size = 0;
            }
            return size + node.Children.Select(GetTheProblemSize).Sum();
        }
    }
}