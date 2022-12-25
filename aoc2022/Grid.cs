using System.Numerics;

namespace aoc2023;

public class Grid<T> where T : INumber<T>
{
    private readonly T[][] _grid;
    public int RowLen { get; }
    public int ColLen { get; }


    public Grid(T[][] grid)
    {
        _grid = grid;
        RowLen = _grid.Length;
        ColLen = _grid.Length;
    }

    public T[] this[int x] => _grid[x];

    public IEnumerable<(T value, int index)> IterateHorizontal(int row, bool startFromEnd = false) =>
        IterateHorizontal(row, startFromEnd ? ColLen - 1 : 0, startFromEnd);

    private IEnumerable<(T value, int index)> IterateHorizontal(int row, int col, bool goBackwards)
    {
        for (var i = col; goBackwards ? i >= 0 : i < _grid[row].Length; i += goBackwards ? -1 : 1)
        {
            yield return (_grid[row][i], i);
        }
    }

    public IEnumerable<(T value, int index)> IterateVertical(int col, bool startFromEnd = false) =>
        IterateVertical(startFromEnd ? RowLen - 1 : 0, col, startFromEnd);

    private IEnumerable<(T value, int index)> IterateVertical(int row, int col, bool goBackwards)
    {
        for (var i = row; goBackwards ? i >= 0 : i < RowLen; i += goBackwards ? -1 : 1)
        {
            yield return (_grid[i][col], i);
        }
    }

    public T Get(int row, int col)
    {
        if (row < RowLen && row >= 0 && col < ColLen && col >= 0)
        {
            return _grid[row][col];
        }

        throw new IndexOutOfRangeException("index out of range");
    }

    public IEnumerable<(T value, int index)> IterateFromIndexToEnd(int row, int col, GridDirection direction)
    {
        return direction switch
        {
            GridDirection.Right => IterateHorizontal(row, col, false),
            GridDirection.Left => IterateHorizontal(row, col, true),
            GridDirection.Down => IterateVertical(row, col, false),
            GridDirection.Up => IterateVertical(row, col, true),
            GridDirection.Backwards => throw new InvalidOperationException(direction.ToString()),
            GridDirection.Forwards => throw new InvalidOperationException(direction.ToString()),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}