using System.Data;

namespace aoc2023;

public sealed class Day5 : DayBase<int>
{
    private IEnumerable<string> input;

    public Day5()
    {
        input = ReadFileAsLines(5, true);
    }

    public override int Solve()
    {
        var stackCollection = new StackCollection<char>();
        foreach (var str in input)
        {
            if (!str.StartsWith(" 1") && !str.StartsWith("move") && !string.IsNullOrEmpty(str))
            {
                var steps = 0;
                for (var i = 0; i < str.Length; i += 4)
                {
                    char c = str[i + 1];
                    if (c != ' ')
                    {
                        stackCollection.Add(steps, c);
                    }

                    steps++;
                }
            }
            else if (string.IsNullOrEmpty(str))
            {
                
            }
            else if (str.StartsWith("move"))
            {
                List<int> temp = new List<int>();
                for (var i = 0; i < str.Length; i++)
                {
                    string s = "";
                    while (i < str.Length && char.IsNumber(str[i]))
                    {
                        s += str[i];
                        i++;
                    }

                    if (!string.IsNullOrEmpty(s))
                    {
                        temp.Add(int.Parse(s));
                    }
                }

                stackCollection.Move(temp[0], temp[1] - 1, temp[2] - 1);
            }
        }

        Console.WriteLine(stackCollection.Result());
        return 0;
    }

    public override int Solve2()
    {
        return 0;
    }
}

public class StackCollection<T>
{
    private readonly Stack<T>[] _stacks = new Stack<T>[9];

    public StackCollection()
    {
        for (var i = 0; i < _stacks.Length; i++)
        {
            _stacks[i] = new Stack<T>();
        }
    }

    public void Add(int index, T value)
    {
        //todo addar i fel ordning eftersom vi läser in stacken uppifrån och ner?
        _stacks[index].Push(value);
    }

    public void Move(int count, int from, int to)
    {
        for (var i = 0; i < count; i++)
        {
            var f = _stacks[from].Pop();
            
            _stacks[to].Push(f);
        }
    }

    public string Result()
    {
        var str = "";
        foreach (var stack in _stacks)
        {
            str += stack.Peek();
        }

        return str;
    }
}