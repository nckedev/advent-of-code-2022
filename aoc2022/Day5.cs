using System.Data;

namespace aoc2023;

public sealed class Day5 : DayBase<int>
{
    private IEnumerable<string> input;

    public Day5()
    {
        input = ReadFileAsLines(5);
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
                stackCollection.Reverse();
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
                stackCollection.Reverse();
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

                stackCollection.MoveStack(temp[0], temp[1] - 1, temp[2] - 1);
            }
        }

        Console.WriteLine(stackCollection.Result());
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

    public void MoveStack(int count, int from, int to)
    {
        var temp = new List<T>();
        
        for (var i = 0; i < count; i++)
        {
            temp.Add(_stacks[from].Pop());
        }
        
        for(int i = temp.Count -1 ; i >= 0; i--)
        {
            _stacks[to].Push(temp[i]);
        }
    }

    public void Reverse()
    {
        for (var i = 0; i < _stacks.Length; i++)
        {
            var tempStack = new Stack<T>();
            var len = _stacks[i].Count;
            for (var j = 0; j < len; j++)
            {
                tempStack.Push(_stacks[i].Pop());
            }

            _stacks[i] = tempStack;
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
//
// LBLVVTVLP
// 1: 0    in 19ms
// TPFFBDRJD
// 2: 0    in 3ms
