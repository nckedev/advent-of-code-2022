using System.ComponentModel.Design.Serialization;
using System.Data;

namespace aoc2023;

public struct Knot
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Knot(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Step(Direction dir)
    {
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

        return visited.Count;
    }

    public override int Solve2()
    {
        return 0;
    }
}