namespace aoc2023;

public enum Hand
{
    Rock, // a x
    Paper, // b y
    Scissors, // c z
    Undefined,
}

public enum Result
{
    Loss, //x 
    Tie, //y
    Win, //z
    Undefined,
}

public struct Game
{
    public Hand Opponent { get; }
    public Hand Player { get; }
    public Result WantedResult { get; }

    public Game(Hand opponent, Hand player)
    {
        Opponent = opponent;
        Player = player;
    }

    public Game(Hand opponent, Result wantedResult)
    {
        Opponent = opponent;
        WantedResult = wantedResult;
    }
}

public class Day2 : DayBase<int>
{
    private Hand ToHand(char c) => c switch
    {
        'A' or 'X' => Hand.Rock,
        'B' or 'Y' => Hand.Paper,
        'C' or 'Z' => Hand.Scissors,
        _ => Hand.Undefined,
    };

    private Result ToWantedResult(char res) => res switch
    {
        'X' => Result.Loss,
        'Y' => Result.Tie,
        'Z' => Result.Win,
        _ => Result.Undefined,
    };

    private static readonly int[,] ScoreMatrix = new int[3, 3]
    {
        {4, 1, 7},
        {8, 5, 2},
        {3, 9, 6},
    };

    private static readonly Hand[,] RequiredHandToMeetWantedResult = new Hand[3, 3]
    {
        {Hand.Scissors, Hand.Rock, Hand.Paper},
        {Hand.Rock, Hand.Paper, Hand.Scissors},
        {Hand.Paper, Hand.Scissors, Hand.Rock}
    };

    private int GetScore(Hand player, Hand opponent) =>
        ScoreMatrix[(int) player, (int) opponent];

    private Hand GetRequiredHand(Hand opponent, Result wantedResult) =>
        RequiredHandToMeetWantedResult[(int) opponent, (int) wantedResult];

    public override int Solve()
    {
        return ReadFileAsLines(2)
            .Select(s => new Game(ToHand(s.First()), ToHand(s.Last())))
            .Sum(s => GetScore(s.Player, s.Opponent));
    }

    public override int Solve2()
    {
        return ReadFileAsLines(2)
            .Select(s => new Game(ToHand(s.First()), ToWantedResult(s.Last())))
            .Sum(s => GetScore(GetRequiredHand(s.Opponent, s.WantedResult), s.Opponent));
    }
}