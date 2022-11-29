using System.Diagnostics;
using System.Numerics;

namespace aoc2023;

public abstract class DayBase<T> where T : INumber<T>
{
    public abstract T Solve();
    public abstract T Solve2();

    public virtual T ReadFile(string file)
    {
        return T.Zero;
    }

    public virtual string GetFile(int day) =>
        "Day" + day + ".txt";
    
    public virtual string GetTestFile(int day) =>
        "Day_test" + day + ".txt";
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