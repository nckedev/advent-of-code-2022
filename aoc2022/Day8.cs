using System.Data;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace aoc2023;

public struct Seen
{
    public int Value;
    public int Row;
    public int Col;

    public Seen(int value, int row, int col)
    {
        Value = value;
        Row = row;
        Col = col;
    }

    public override int GetHashCode()
    {
        return Value + Row * 10 + Col * 100;
    }
}

public class Day8 : DayBase<int>
{
    private HashSet<Seen> Seen = new HashSet<Seen>();
    private int _currentMax = 0;

    private int CheckValue(int value, int row, int col)
    {
        if ((value > _currentMax))
        {
            var seen = new Seen(value, row, col);
            if (!Seen.Contains(seen))
            {
                Seen.Add(seen);
                _currentMax = value;
            }
        }

        return value;
    }

    public override int Solve()
    {
        var f = ReadFileAsLines(8, true);
        var sum = 0;
        var grid = new Grid<int>(f.Count());
        foreach (var row in f)
        {
            grid.Add(row);
        }

        foreach (var a in Enumerable.Range(0, grid.Rows))
        {
            foreach (var b in grid.IterateHorizontal(a))
            {
                CheckValue(b.value, a, b.index);

                if (b.value == 9) break;
            }

            sum += Seen.Count;
            _currentMax = 0;

            foreach (var b in grid.IterateHorizontal(a, true))
            {
                CheckValue(b.value, a, b.index);

                if (b.value == 9) break;
            }

            sum += Seen.Count;
            _currentMax = 0;
        }

        foreach (var a in Enumerable.Range(0, grid.Cols))
        {
            foreach (var b in grid.IterateVertical(a))
            {
                CheckValue(b.value, b.index, a);

                if (b.value == 9) break;
            }

            sum += Seen.Count;
            _currentMax = 0;
            foreach (var b in grid.IterateVertical(a, true))
            {
                CheckValue(b.value, b.index, a);

                if (b.value == 9) break;
            }

            sum += Seen.Count;
            _currentMax = 0;
        }

        return Seen.Count;
        //1578 för lågt
        //1745 för högt
    }

    public override int Solve2()
    {
        return 0;
    }

    private void PrittyPrint(HashSet<Seen> seen, int size = 5)
    {
        var list = new int[size][];
        for (int i = 0 ; i > size ; i ++)
        {
            list[i] = Enumerable.Repeat(-1, size).ToArray();
        }
        
        
    }
}

public class Grid<T> where T : INumber<T>
{
    private T[][] grid;
    public int Rows { get; }
    public int Cols { get; private set; }

    private int _currentRow = 0;

    public Grid(int rows)
    {
        Rows = rows;
        grid = new T[rows][];
    }

    public void Add(string row)
    {
        grid[_currentRow] = row.Select(s => T.Parse(s.ToString(), null)).ToArray();
        Cols = grid[_currentRow].Length;
        _currentRow++;
    }

    public IEnumerable<(T value, int index)> IterateHorizontal(int row, bool startFromEnd = false)
    {
        if (!startFromEnd)
        {
            for (var i = 0; i < grid[row].Length ; i++)
            {
                yield return (grid[row][i], i);
            }
        }

        else
        {
            for (var i = grid[row].Length - 1; i >= 0; i--)
            {
                yield return (grid[row][i], i);
            }
        }
    }

    public IEnumerable<(T value, int index)> IterateVertical(int col, bool startFromEnd = false)
    {
        if (!startFromEnd)
        {
            for (var i = 0; i < Rows; i++)
            {
                yield return (grid[i][col], i);
            }
        }
        else
        {
            for (var i = Rows - 1; i >= 0; i--)
            {
                yield return (grid[i][col], i);
            }
        }
    }
}