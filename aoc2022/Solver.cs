using System.Diagnostics;
using System.Numerics;

namespace aoc2023;

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