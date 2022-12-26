using System.ComponentModel.Design.Serialization;
using System.Data;

namespace aoc2023;

public struct Knot
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int LastX { get; private set; }
    public int LastY { get; private set; }

    public Knot(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Step(Direction dir)
    {
        LastX = X;
        LastY = Y;
        if (dir == Direction.Up) Y++;
        if (dir == Direction.Down) Y--;
        if (dir == Direction.Left) X--;
        if (dir == Direction.Right) X++;
    }

    public bool IsAdj(Knot k)
    {
        var xDiff = Math.Abs(X - k.X);
        var yDiff = Math.Abs(Y - k.Y);

        return xDiff <= 1 && yDiff <= 1;
    }

    public void Follow(Knot k)
    {
        X = k.LastX;
        Y = k.LastY;
    }

    private int ManhattanDistanceTo(Knot k)
    {
        return Math.Abs(k.X - X) + Math.Abs(k.Y - Y);
    }
}

public record struct Instruction(Direction Direction, int Count);

public enum Direction
{
    Undefined,
    Up,
    Down,
    Left,
    Right
}

public class Day9 : DayBase<int>
{
    private Direction CharToDirection(char c) => c switch
    {
        'R' => Direction.Right,
        'U' => Direction.Up,
        'L' => Direction.Left,
        'D' => Direction.Down,
        _ => Direction.Undefined
    };

    private void PrintTrail(HashSet<Knot> set)
    {
        var maxY = set.Max(m => m.Y) + 1;
        var maxX = set.Max(m => m.X) + 1;

        var grid = new List<char[]>();
        for (var i = 0; i < maxY; i++)
        {
            grid.Add(Enumerable.Repeat('.', maxX).ToArray());
        }
        foreach (var s in set)
        {
            grid[maxY -1 - s.Y][s.X] = '#';
        }

        foreach (var row in grid)
        {
            foreach (var col in row)
            {
                Console.Write(col);
            }
            Console.WriteLine();
        }
    }

    public override int Solve()
    {
        var head = new Knot(0, 0);
        var tail = new Knot(0, 0);

        var visited = new HashSet<Knot>();
        foreach (var instruction in ReadFileAsLines(9, true).Select(s =>
                 {
                     var t = s.Split(' ');
                     return new Instruction(CharToDirection(t.First()[0]), int.Parse(t[1]));
                 }))
        {
            for (var i = 0; i < instruction.Count; i++)
            {
                head.Step(instruction.Direction);
                visited.Add(tail);
                if (!tail.IsAdj(head))
                {
                    tail.Follow(head);
                }
            }
        }

        //6270 to low
        PrintTrail(visited);
        return visited.Count;
    }

    public override int Solve2()
    {
        return 0;
    }
}