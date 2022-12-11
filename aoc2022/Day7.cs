namespace aoc2023;

public enum Type
{
    Undefined,
    File, 
    Dir,
}
public struct AocFile
{
    public string Name { get; }
    public int Size { get; }

    public AocFile(string name, int size)
    {
        Name = name;
        Size = size;
    }
}

public class Day7 : DayBase<int>
{
    public override int Solve()
    {
        return 0;
    }

    public override int Solve2()
    {
        return 0;
    }
}