namespace aoc2023;

public class Day3 : DayBase<int>
{
    private long first;
    private long second;

    public override int Solve()
    {
        var list = ReadFileAsLines(3).ToList();
        foreach (var row in list)
        {
            for (int i = 0; i < row.Count(); i++)
            {
                if (i <= row.Length / 2)
                {
                    first |= (uint) (1 << (row[i] - 'a'));
                }
                else
                {
                    second |= (uint) (1 << (row[i] - 'a'));
                }
            }

            var res = first & second;
            var b = 1;
        }
        return 0;
    }

    public override int Solve2()
    {
        return 0;
    }
}