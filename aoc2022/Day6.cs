namespace aoc2023;

public sealed class Day6 : DayBase<int>
{
    private readonly string _input;

    public Day6()
    {
        _input = ReadFileAsLines(6).First();
        // _input = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg"; // 10
    }

    private int UniqueSeq(int len)
    {
        for (var i = 0; i < _input.Length - len; i++)
        {
            var range = _input[new Range(i, i + len)];
            if (range.Distinct().Count() == len)
                return i + len;
        }

        return 0;
    }

    public override int Solve()
    {
        return UniqueSeq(4);
    }

    public override int Solve2()
    {
        return UniqueSeq(14);
    }
}