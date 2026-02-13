using Frogobot.Data.Dto;
using Frogobot.Data.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using User = NetCord.User;

namespace Frogobot.Core.Commands;

public class PossessPointCommands : ApplicationCommandModule<ApplicationCommandContext>
{
	private readonly IPossessPointService _possessPointService;
	private readonly DiscordEmojiOptions _emoji;
	
	[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
	public PossessPointCommands(IPossessPointService possessPointService, IOptions<DiscordEmojiOptions> emojiOptions)
	{
		_possessPointService = possessPointService;
		_emoji = emojiOptions.Value;
	}
	
	[SlashCommand("possess", "Ajoute un Possess-Point à la personne choisie")]
	public async Task<string> Possess(User? user = null)
	{
		user ??= Context.User;
		var total = await _possessPointService.AddPossessPointAsync(user.Id);
		
		return $"{user} a mentionné Possesslime ! {_emoji.PossessPoint}\nIl a maintenant **{total} Possess-Points**.";
	}
	
	[SlashCommand("scoreboard", "Affiche le tableau des Possess-Points")]
	public async Task<InteractionMessageProperties> Scoreboard()
	{
		PossessScoreboardRank[] scoreboardRanks = await _possessPointService.GetPossessScoreboardAsync(5);

		var description = string.Join("\n", scoreboardRanks.Select(rank =>
		{
			var medal = rank.Rank switch
			{
				1 => "🥇",
				2 => "🥈",
				3 => "🥉",
				_ => $"**#{rank.Rank}**"
			};
			return $"{medal} <@{rank.UserId}> : {rank.PossessPoints} {_emoji.PossessPoint}";
		}));

		if (string.IsNullOrEmpty(description))
			description = "*Aucun point n'est enregistré, ...pour le moment*";

		var embed = new EmbedProperties()
		{
			Title = "🏆 Tableau des Posses-Points",
			Description = description,
			Color = new Color(0x9B59B6),
			Footer = new EmbedFooterProperties
			{
				Text = "Top 5 des mentionneurs de Possesslime !"
			},
			Timestamp = DateTimeOffset.UtcNow
		};

		return new InteractionMessageProperties
		{
			Embeds = [embed]
		};
	}

	[SlashCommand("points", "Affiche les Possess-Points de la personne choisie")]
	public async Task<string> Points(User? user = null)
	{
		user ??= Context.User;
		var points = await _possessPointService.GetUserPossessPointsAsync(user.Id);

		return points <= 0
			? $"{user} n'a jamais mentionné Possesslime !"
			: $"{user} a gagné {points} {_emoji.PossessPoint} Possess-Points !";
	}
}
