using Frogobot.Data.Dto;
using Frogobot.Data.Services;
using JetBrains.Annotations;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Frogobot.Core.Commands;

public class GameFinderCommands : ApplicationCommandModule<ApplicationCommandContext>
{
	private readonly IGameFinderService _gameFinderService;
	
	[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
	public GameFinderCommands(IGameFinderService gameFinderService)
	{
		_gameFinderService = gameFinderService;
	}

	[SlashCommand("registergame", "Enregistre un jeu")]
	public async Task<string> RegisterGame(string gameName,
		uint baseMaxPlayerCount,
		bool allowCustomLobbies = false,
		uint? maxTeamCount = null,
		uint? maxPlayersPerTeam = null,
		bool supportsModsForMorePlayers = false,
		uint? moddedMaxPlayerCount = null)
	{
		var option = new RegisterGameOption(gameName, baseMaxPlayerCount, allowCustomLobbies,
			maxTeamCount, maxPlayersPerTeam, supportsModsForMorePlayers, moddedMaxPlayerCount);
		
		await _gameFinderService.RegisterGameAsync(option);
		return $"Jeu {option.GameName} enregistré avec succès";
	}

	[SlashCommand("owngame", "Ajoute un jeu à la liste de jeux de l'utilisateur")]
	public async Task<string> OwnGame(string gameName, User? user = null)
	{
		if (user == null)
			user = Context.User;
		
		var results = await _gameFinderService.SetUserAsOwnerAsync(user.Id, gameName);
		if (!results)
			return $"Le jeu {gameName} n'a pas été trouvé.";
		
		return $"Jeu {gameName} ajouté à la liste de jeux de l'utilisateur {user.Username}";
	}
	
	[SlashCommand("findgame", "Trouve des jeux avec les personnes choisies")]
	public async Task<InteractionMessageProperties> FindGame(List<User> users, bool allowCustoms = true, bool allowSplit = true, bool allowMods = true)
	{
		GameSearchSettings settings = new GameSearchSettings(users.Select(u => u.Id).ToArray(), allowCustoms, allowSplit, allowMods);
		var found = await _gameFinderService.GetSharedGamesAsync(settings);
		
		if (found.Games.Count <= 0)
			return "Aucun jeu trouvé pour les critères sélectionnés.";
		
		var description = found.Games.Select(g => g.GameName).Aggregate((a, b) => $"{a}, {b}");
		var embed = new EmbedProperties()
		{
			Title = "Jeux en commun",
			Description = description,
			Color = new Color(0x9B59B6),
			Timestamp = DateTimeOffset.UtcNow
		};

		return new InteractionMessageProperties
		{
			Embeds = [embed]
		};
	}
}
