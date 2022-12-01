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

    public virtual string GetFile(int day) =>
        "input/Day" + day + ".txt";
    
    public virtual string GetTestFile(int day) =>
        "input/Day_test" + day + ".txt";
}


public static class Solver
{
    private static long totalTime;
    public static void Solve<T>(DayBase<T> day) where T : INumber<T>
    {
        Stopwatch s = new Stopwatch();
        s.Start();
        var res = day.Solve();
        s.Stop();
        Console.Write("1: " + res);
        Console.WriteLine("    in " + s.Elapsed.Milliseconds + "ms");
        totalTime += s.Elapsed.Milliseconds;
        s.Restart();
        var res2 = day.Solve2();
        s.Stop();
        Console.Write("2: " + res2);
        Console.WriteLine("    in " + s.Elapsed.Milliseconds + "ms");
        totalTime += s.Elapsed.Milliseconds;
        
        Console.WriteLine("total Time: " + totalTime + "ms");
    }

    public static void SolveAll()
    {
        Console.WriteLine("not implemented");
    }
}

public static class Utils
{
    
}