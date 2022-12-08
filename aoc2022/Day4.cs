namespace aoc2023;

public struct Intervall
{
    public Intervall(int start, int end)
    {
        Start = start;
        End = end;
    }

    private int Start { get; }
    private int End { get; }

    public bool Contains(Intervall intervall)
    {
        return intervall.Start >= Start && intervall.End <= End;
    }

    public bool Overlaps(Intervall intervall)
    {
        return intervall.Start >= Start && intervall.Start <= End ||
               intervall.End >= Start && intervall.End <= End;
    }
}

public struct Pair
{
    private Intervall a;
    private Intervall b;

    public Pair(Intervall a, Intervall b)
    {
        this.a = a;
        this.b = b;
    }

    public bool HasFullyOverlap()
    {
        return a.Contains(b) || b.Contains(a);
    }

    public bool HasPartialOverlap()
    {
        return a.Overlaps(b) || b.Overlaps(a);
    }
}

public sealed class Day4 : DayBase<int>
{
    private IEnumerable<string> list;

    public Day4()
    {
        list = ReadFileAsLines(4);
    }

    public override int Solve()
    {
        var count = 0;
        foreach (var row in list)
        {
            var temp = row.Split(',');
            var intervallA = temp[0].Split('-');
            var A = new Intervall(int.Parse(intervallA[0]), int.Parse(intervallA[1]));

            var intervallB = temp[1].Split('-');
            var B = new Intervall(int.Parse(intervallB[0]), int.Parse(intervallB[1]));

            if (new Pair(A, B).HasFullyOverlap())
                count++;
        }

        return count;
    }

    public override int Solve2()
    {
        var count = 0;
        foreach (var row in list)
        {
            var temp = row.Split(',');
            var intervallA = temp[0].Split('-');
            var A = new Intervall(int.Parse(intervallA[0]), int.Parse(intervallA[1]));

            var intervallB = temp[1].Split('-');
            var B = new Intervall(int.Parse(intervallB[0]), int.Parse(intervallB[1]));

            if (new Pair(A, B).HasPartialOverlap())
                count++;
        }

        return count;
    }
}