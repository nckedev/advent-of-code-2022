using System.Data;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

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
        return base.GetHashCode();
        // return Value + Row * 10 + Col * 100;
    }
}

public sealed class Day8 : DayBase<int>
{
    private HashSet<Seen> Seen = new HashSet<Seen>();
    private int _currentMax = -1;

    private readonly Grid<int> _grid;

    public Day8()
    {
        _grid = new Grid<int>(ReadFileAsGrid(8, false));
    }

    private void CheckValue(int value, int row, int col)
    {
        if (value > _currentMax)
        {
            _currentMax = value;
            var seen = new Seen(value, row, col);
            if (!Seen.Contains(seen))
            {
                Seen.Add(seen);
            }
        }
    }

    private void CheckValues(int index, bool isCol, bool fromBack,
        Func<int, bool, IEnumerable<(int Value, int Index)>> f)
    {
        foreach (var t in f.Invoke(index, fromBack))
        {
            CheckValue(t.Value, isCol ? t.Index : index, isCol ? index : t.Index);
            if (t.Value == 9) break;
        }

        _currentMax = -1;
    }

    public override int Solve()
    {
        foreach (var a in Enumerable.Range(0, _grid.RowLen))
        {
            CheckValues(a, false, false, (i, b) => _grid.IterateHorizontal(i, b));
            CheckValues(a, false, true, (i, b) => _grid.IterateHorizontal(i, b));
        }

        foreach (var a in Enumerable.Range(0, _grid.ColLen))
        {
            CheckValues(a, true, false, (i, b) => _grid.IterateVertical(i, b));
            CheckValues(a, true, true, (i, b) => _grid.IterateVertical(i, b));
        }

        // PrettyPrint(Seen);
        return Seen.Count;
    }

    public override int Solve2()
    {
        var maxScore = 0;
        for (var i = 0; i < _grid.RowLen; i++)
        {
            for (var j = 0; j < _grid.ColLen; j++)
            {
                var score = 1;
                var currentHeight = _grid.Get(i, j);
                for (var k = 0; k < 4; k++)
                {
                    var isFirst = true;

                    var steps = 0;
                    foreach (var l in _grid.IterateFromIndexToEnd(i, j, (GridDirection) (1 << k)))
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            continue;
                        }
                        steps++;

                        var isBackwards = ((GridDirection) k & GridDirection.Backwards) > 0;
                        var maxIndex = _grid.RowLen - 1;

                        if (l.value >= currentHeight
                            || (isBackwards && l.index == 0)
                            || (!isBackwards && l.index == maxIndex))
                        {
                            break;
                        }
                    }
                    score *= steps;

                    if(score == 0) break;
                }

                if (score > maxScore)
                {
                    maxScore = score;
                }
            }
        }

        return maxScore;
    }

    private void PrettyPrint(HashSet<Seen> seen, int size = 5)
    {
        var list = new int[size][];
        for (int i = 0; i < size; i++)
        {
            list[i] = Enumerable.Repeat(-1, size).ToArray();
        }

        foreach (var s in Seen)
        {
            list[s.Row][s.Col] = s.Value;
        }

        foreach (var row in list)
        {
            foreach (var col in row)
            {
                if (col == -1)
                {
                    Console.Write("x ");
                }
                else
                {
                    Console.Write(col + " ");
                }
            }

            Console.WriteLine();
        }
    }
}