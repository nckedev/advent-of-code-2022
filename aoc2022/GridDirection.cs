namespace aoc2023;

[Flags]
public enum GridDirection
{
    Right = 1 << 0,
    Left = 1 << 1,
    Up = 1 << 2,
    Down = 1 << 3,
    Backwards = Left | Up,
    Forwards = Right | Down,
}