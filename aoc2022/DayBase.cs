using System.Diagnostics;

namespace aoc2023;

public abstract class DayBase
{
    public abstract long Solve();
    public abstract long Solve2();

    public virtual long? ReadFile(string file)
    {
        return null;
    }

    public virtual string GetFile(int day) =>
        "Day" + day + ".txt";
    
    public virtual string GetTestFile(int day) =>
        "Day_test" + day + ".txt";
}


public static class Solver
{
    public static void Solve(DayBase day)
    {
        Stopwatch s = new Stopwatch();
        s.Start();
        Console.Write("1: " + day.Solve());
        Console.WriteLine("    in " + s.Elapsed.Milliseconds + "ms");
        s.Stop();
        s.Restart();
        Console.Write("2: " + day.Solve2());
        Console.WriteLine("    in " + s.Elapsed.Milliseconds + "ms");
        s.Stop();
    }
}

public static class Utils
{
    
}