namespace aoc2023;

public sealed class Day3 : DayBase<ulong>
{
    private ulong first;
    private ulong second;
    private List<string> list;

    public Day3()
    {
        list = ReadFileAsLines(3).ToList();
    }

    public override ulong Solve()
    {
        var res = 0UL;

        foreach (var row in list)
        {
            for (var i = 0; i < row.Length; i++)
            {
                if (i < row.Length / 2)
                {
                    first |= 1UL << CharToIndex(row[i]);
                }
                else
                {
                    second |= 1UL << CharToIndex(row[i]);
                }
            }

            res += (ulong) Math.Log2(first & second) + 1;
            first = second = 0;
        }

        return res;
    }


    private void PrintBinary(ulong value)
    {
        var str = "";
        var hasAtleastOneBitSet = false;
        for (var i = 0; i < 64; i++)
        {
            if ((value & (1UL << i)) != 0)
            {
                str += "1";
                hasAtleastOneBitSet = true;
            }
            else
            {
                str += "0";
            }
        }

        if (!hasAtleastOneBitSet) throw new Exception("ingen etta satt");
        Console.WriteLine(str);
    }

    //a = 0, Z = 51
    private static int CharToIndex(char c) =>
        char.IsUpper(c) ? c - 'A' + 26 : c - 'a';

    public override ulong Solve2()
    {
        var count = 1;
        var ans = ulong.MaxValue;
        var t = 0UL;
        var score = 0;
        foreach (var listrow in list)
        {
            for (var i = 0; i < listrow.Length; i++)
            {
                t |= 1UL << CharToIndex(listrow[i]);
            }

            ans &= t;
            t = 0UL;

            if (count % 3 == 0)
            {
                score += (int) Math.Log2(ans) + 1;
                ans = ulong.MaxValue;
            }

            count++;
        }

        return (ulong) score;
    }
}