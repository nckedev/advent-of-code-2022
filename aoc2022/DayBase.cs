using System.Diagnostics;
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

    public virtual IEnumerable<string> ReadFileAsLines(int day, bool testfile= false)
    {
        if (testfile) return File.ReadLines(GetTestFile(day));
        return File.ReadLines(GetFile(day));
    }

    public virtual string GetFile(int day) =>
        "input/Day" + day + ".txt";

    public virtual string GetTestFile(int day) =>
        "input/Day" + day + "_test.txt";

    public void PrintIf(bool exp, string str)
    {
        if (exp)
            Console.WriteLine(">>> printIf >>> " + str);
    }
}

public static class Solver
{
    private static long _totalTime;

    public static void Solve<T>(DayBase<T> day) where T : INumber<T>
    {
        var name = day.GetType().FullName;
        Console.WriteLine(name);
        var s = new Stopwatch();
        s.Start();
        var res = day.Solve();
        s.Stop();
        Console.Write("1: " + res);
        Console.WriteLine("    in " + s.Elapsed.Milliseconds + "ms");
        _totalTime += s.Elapsed.Milliseconds;
        
        
        s.Restart();
        var res2 = day.Solve2();
        s.Stop();
        Console.Write("2: " + res2);
        Console.WriteLine("    in " + s.Elapsed.Milliseconds + "ms");
        Console.WriteLine();
        _totalTime += s.Elapsed.Milliseconds; 

    }

    public static void PrintTotalTime() => Console.WriteLine("Total time: " + _totalTime + "ms");

    public static void SolveAll()
    {
        Console.WriteLine("not implemented");
    }
}

public static class Utils
{
}