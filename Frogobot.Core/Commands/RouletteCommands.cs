using Frogobot.Core.Utils;
using Frogobot.Data.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using NetCord.Services.ApplicationCommands;

namespace Frogobot.Core.Commands;

public class RouletteCommands : ApplicationCommandModule<ApplicationCommandContext>
{
    private readonly IPossessPointService _possessPointService;
    private readonly IRouletteService _rouletteService;
    private readonly DiscordEmojiOptions _emoji;

    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public RouletteCommands(IPossessPointService possessPointService, IRouletteService rouletteService, IOptions<DiscordEmojiOptions> emojiOptions)
    {
        _possessPointService = possessPointService;
        _rouletteService = rouletteService;
        _emoji = emojiOptions.Value;
    }

    [SlashCommand("roulette", "Misez vos Possess-Points à la roulette !")]
    public async Task<string> Roulette(
        [SlashCommandParameter(Name = "mise", Description = "La mise en Possess-Points")] int bet, 
        [SlashCommandParameter(Name = "type", Description = "Le type de pari à effectuer")] RouletteBetType type,
        [SlashCommandParameter(Name = "numéro", Description = "Le numéro à miser (pour le pari sur un nombre)")] int? number = null)
    {
        if (bet <= 0)
            return "Vous devez miser au moins 1 point !";

        if (type == RouletteBetType.Number && (number is < 0 or > 36))
            return "Le numéro doit être compris entre 0 et 36 !";

        if (type == RouletteBetType.Number && number == null)
            return "Vous devez spécifier un numéro pour ce type de pari !";

        var userPoints = await _possessPointService.GetUserPossessPointsAsync(Context.User.Id);

        if (userPoints < bet)
            return $"Vous n'avez pas assez de points ! (Points actuels : **{userPoints}** {_emoji.PossessPoint})";

        var result = _rouletteService.Play(bet, type, number);
        var newTotal = await _possessPointService.IncreasePossessPointsAsync(Context.User.Id, result.Delta);

        string rollInfo = $"Le numéro tiré est le **{result.RolledNumber}**.";

        if (result.IsWin)
            return $"🎰 **GAGNÉ !** 🎰\n{rollInfo}\nVous avez gagné **{result.Delta}** {_emoji.PossessPoint} !\nVotre nouveau solde est de **{newTotal}** {_emoji.PossessPoint}.";
        
        return $"💀 **PERDU...** 💀\n{rollInfo}\nVous avez perdu **{bet}** {_emoji.PossessPoint}...\nVotre nouveau solde est de **{newTotal}** {_emoji.PossessPoint}.";
    }
}
