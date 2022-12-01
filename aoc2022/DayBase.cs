using System.Diagnostics;
using System.Numerics;
using System.Windows;

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
    public static void Solve<T>(DayBase<T> day) where T : INumber<T>
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