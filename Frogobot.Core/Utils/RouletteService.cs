using NetCord.Services.ApplicationCommands;

namespace Frogobot.Core.Utils;

public enum RouletteBetType
{
    [SlashCommandChoice(Name = "Rouge")]
    Red,
    [SlashCommandChoice(Name = "Noir")]
    Black,
    [SlashCommandChoice(Name = "Pair")]
    Even,
    [SlashCommandChoice(Name = "Impair")]
    Odd,
    [SlashCommandChoice(Name = "Nombre")]
    Number
}

public interface IRouletteService
{
    RouletteResult Play(int bet, RouletteBetType type, int? number = null);
}

public record RouletteResult(bool IsWin, int Delta, int? RolledNumber = null);

public class RouletteService : IRouletteService
{
    private readonly Random _random;

    private static readonly int[] RedNumbers = [1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36];

    public RouletteService(Random? random = null)
    {
        _random = random ?? Random.Shared;
    }

    public RouletteResult Play(int bet, RouletteBetType type, int? number = null)
    {
        int roll = _random.Next(0, 37);

        bool isWin = false;
        int multiplier = 0;

        switch (type)
        {
            case RouletteBetType.Red:
                isWin = roll != 0 && RedNumbers.Contains(roll);
                multiplier = 2;
                break;
            case RouletteBetType.Black:
                isWin = roll != 0 && !RedNumbers.Contains(roll);
                multiplier = 2;
                break;
            case RouletteBetType.Even:
                isWin = roll != 0 && roll % 2 == 0;
                multiplier = 2;
                break;
            case RouletteBetType.Odd:
                isWin = roll != 0 && roll % 2 != 0;
                multiplier = 2;
                break;
            case RouletteBetType.Number:
                isWin = roll == number;
                multiplier = 36;
                break;
        }

        int delta = isWin ? bet * (multiplier - 1) : -bet;

        return new RouletteResult(isWin, delta, roll);
    }
}
