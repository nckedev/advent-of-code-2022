using System.Net.Sockets;

namespace aoc2023;

public enum InstructionType
{
    Undef,
    Addx,
    Noop,
}

public struct CpuInstruction
{
    public InstructionType Type { get; }
    public int Value { get; }

    public CpuInstruction(string inst, string value = "0")
    {
        Type = ToInstruction(inst);
        Value = int.Parse(value);
    }

    private InstructionType ToInstruction(string s) => s switch
    {
        "addx" => InstructionType.Addx,
        "noop" => InstructionType.Noop,
        _ => InstructionType.Undef
    };
}

public class Cpu
{
    private readonly IEnumerable<CpuInstruction> _program;

    private readonly Dictionary<InstructionType, int> _cost;

    private int _cycle = 0;
    private int X = 1;

    public Cpu(IEnumerable<CpuInstruction> program)
    {
        _program = program;
        _cost = new Dictionary<InstructionType, int>()
        {
            { InstructionType.Addx, 2 },
            { InstructionType.Noop, 1 },
        };
    }

    private int GetCost(InstructionType type)
    {
        if (_cost.TryGetValue(type, out var cost))
        {
            return cost;
        }

        return 0;
    }

    public IEnumerable<int> Run()
    {
        var counter = 0;
        foreach (var p in _program)
        {
            X += p.Value;
            if (_cycle % 40 == 0 && _cycle != 0)
            {
                yield return X * _cycle;
            }

            _cycle += GetCost(p.Type);
        }
    }

    public IEnumerable<string> Print()
    {
        yield return "";
    }
}

public sealed class Day10 : DayBase<int>
{
    private readonly IEnumerable<string> _input;

    public Day10()
    {
        _input = ReadFileAsLines(10, true);
    }

    public override int Solve()
    {
        var list = _input.Select(s =>
        {
            var split = s.Split(' ');
            return new CpuInstruction(split[0], split.Length == 2 ? split[1] : "0");
        });

        var cpu = new Cpu(list);
        foreach (var c in cpu.Run())
        {
            Console.WriteLine(c);
        }

        return 0;
    }

    public override int Solve2()
    {
        return 0;
    }
}