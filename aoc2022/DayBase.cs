using System.Numerics;

namespace aoc2023;

public abstract class DayBase<T> where T : INumber<T>
{
    public abstract T Solve();
    public abstract T Solve2();

    public virtual IEnumerable<T> ReadFile(int day)
    {
        return File.ReadLines(GetFile(day)).Select(s => T.Parse(s, null));
    }

    protected virtual IEnumerable<string> ReadFileAsLines(int day, bool useTestfile = false)
    {
        return File.ReadLines(GetFile(day, useTestfile));
    }

    protected virtual T[][] ReadFileAsGrid(int day, bool testFile = false)
    {
        var content = File.ReadLines(GetFile(day, testFile));
        var grid = new T[content.Count()][];
        var i = 0;

        foreach (var c in content)
        {
            grid[i] = c.Select(s => T.CreateChecked(s - '0')).ToArray();
            i++;
        }

        return grid;
    }


    protected virtual string GetFile(int day, bool useTestFile = false) =>
        useTestFile ? "input/Day" + day + "_test.txt" : "input/Day" + day + ".txt";

    public void PrintIf(bool exp, string str)
    {
        if (exp)
            Console.WriteLine(">>> printIf >>> " + str);
    }
}

public static class Utils
{
}