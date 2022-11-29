using System.Diagnostics;

namespace aoc2023;


public class Day1 : DayBase<long>
{
    public override long Solve()
    {
        Debug.Assert(1 > 2, "1 är inte större än 2");
        return 1 + 1;
    }

    public override long Solve2()
    {
        throw new NotImplementedException();
    }
}