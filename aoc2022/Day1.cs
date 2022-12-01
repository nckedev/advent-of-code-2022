namespace aoc2023;

public sealed class Day1 : DayBase<long>
{
    private readonly List<string> _input;

    public Day1()
    {
        _input = File.ReadLines(GetFile(1)).ToList();
    }

    public override long Solve()
    {
        var max = 0L;
        var temp = 0L;

        foreach (var s in _input)
        {
            if (!string.IsNullOrEmpty(s))
            {
                temp += long.Parse(s);
            }
            else
            {
                if (temp > max)
                {
                    max = temp;
                }

                temp = 0;
            }
        }

        if (temp > max) max = temp;

        return max;
    }

    public override long Solve2() =>
        _input.Aggregate(new List<long> {0}, (list, str) =>
        {
            if (string.IsNullOrEmpty(str))
            {
                list.Add(0);
            }
            else
            {
                list[^1] += long.Parse(str);
            }

            return list;
        }).OrderByDescending(x => x).Take(3).Sum();
}