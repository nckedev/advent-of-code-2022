namespace aoc2023;

public enum Type
{
    Undefined,
    File,
    Dir,
}

public class Node
{
    public DirNode? Parent { get; }
    public string Name { get; }

    public Node(string name, DirNode? parent)
    {
        Name = name;
        Parent = parent;
    }
}

public class FileNode : Node
{
    public int Size { get; }

    public FileNode(string name, int size, DirNode? parent) : base (name, parent)
    {
        Size = size;
    }
}

public class DirNode : Node
{
    public List<Node> Nodes { get; }
    public int DirSize { get; set; }

    public DirNode(string name, DirNode? parent) : base(name, parent)
    {
        Nodes = new List<Node>();
    }
}

public class Day7 : DayBase<int>
{
    private DirNode Root;

    public Day7()
    {
        Root = Parse();
    }
    
    private DirNode Parse()
    {
        DirNode? current = new DirNode("/", null);
        foreach (var row in ReadFileAsLines(7, true))
        {
            if( row == "$ cd /")
                continue;

            if (row == "$ cd ..")
            {
                if (current?.Parent != null)
                {
                    current = current.Parent;
                }
            }

            if (row.StartsWith("$ cd ") && !row.StartsWith("$ cd .."))
            {
                var path = row[5..];
                current = current?.Nodes?.FirstOrDefault(x => x.Name == path) as DirNode;
            }
            
            if (row.StartsWith("dir "))
            {
                var path = row[4..];
                current?.Nodes?.Add(new DirNode(path, current));
            }

            if (char.IsDigit(row.First()))
            {
                var values = row.Split(" ");
                current?.Nodes?.Add(new FileNode(values[1], int.Parse(values[0]), current));
            }

            if (current == null)
            {
                var b = 1;
            }
        }

        while (current?.Parent != null)
        {
            current = current.Parent;
        }
        return current;
    }

    private int Sum(DirNode? node)
    {
        int sum = 0;
        

        foreach (var n in node.Nodes)
        {
            if (n is DirNode dir)
            {
                return sum + Sum(dir);
            }
            else if (n is FileNode f)
            {
                sum += f.Size;
            }
        }
        return Sum(node.Parent);
    }
    public override int Solve()
    {
        
        return Sum(Root); // 48381165
    }

    public override int Solve2()
    {
        return 0;
    }
}