using Frogobot.Data.Dto;
using Frogobot.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Frogobot.Data.Services;

public class GameFinderService : IGameFinderService
{
	private readonly FrogoContext _db;
	
	public GameFinderService(FrogoContext db)
	{
		_db = db;
	}
	
	public async ValueTask RegisterGameAsync(RegisterGameOption option)
	{
		var game = new Game
		{
			GameName = option.GameName,
			BaseMaxPlayerCount = option.BaseMaxPlayerCount,
			AllowCustomLobbies = option.AllowCustomLobbies,
			MaxTeamCount = option.MaxTeamCount,
			MaxPlayersPerTeam = option.MaxPlayersPerTeam,
			SupportsModsForMorePlayers = option.SupportsModsForMorePlayers,
			ModdedMaxPlayerCount = option.ModdedMaxPlayerCount
		};

		_db.Games.Add(game);
		await _db.SaveChangesAsync();
	}

	public async ValueTask<bool> SetUserAsOwnerAsync(ulong userId, string gameName)
	{
		var game = await _db.Games.Where(g => g.GameName == gameName).FirstOrDefaultAsync();
		if (game == null)
			return false;

		var userGame = new UserGame()
		{
			DiscordUserId = userId,
			GameId = game.GameId,
		};
		
		var user = await _db.Users.FindAsync(userId);
		if (user is null)
		{
			user = new User { DiscordUserId = userId };
			_db.Users.Add(user);
		}
		
		await _db.UserGames.AddAsync(userGame);
		await _db.SaveChangesAsync();
		
		return true;
	}

	public Task<GameInfo[]> GetOwnedGamesAsync(ulong userId)
	{
		return _db.UserGames.Where(ug => ug.DiscordUserId == userId)
			.Select(ug => new GameInfo(ug.Game.GameName,
				ug.Game.BaseMaxPlayerCount,
				ug.Game.AllowCustomLobbies,
				ug.Game.ModdedMaxPlayerCount,
				ug.Game.MaxPlayersPerTeam,
				ug.Game.SupportsModsForMorePlayers,
				ug.Game.ModdedMaxPlayerCount)
			).ToArrayAsync();
	}

	public async Task<SharedGamesInfo> GetSharedGamesAsync(GameSearchSettings settings)
	{
		var userIds = settings.UserIds;
		Dictionary<GameInfo, int> sharedGames = [];
		
		foreach (var userId in userIds)
		{
			GameInfo[] games = await GetOwnedGamesAsync(userId);
			foreach (var game in games)
			{
				if (!sharedGames.TryAdd(game, 1))
					sharedGames[game]++;
			}
		}
		
		List<GameInfo> finalList = sharedGames
			.Where(kvp => kvp.Value == userIds.Length && CanGameBeSelected(kvp.Key, settings))
			.Select(kvp => kvp.Key)
			.ToList();
		
		return new SharedGamesInfo(userIds, finalList);
	}

	private static bool CanGameBeSelected(GameInfo game, GameSearchSettings settings)
	{
		var playerCount = settings.UserIds.Length;
		
		if (CanFitTogether(playerCount, game.BaseMaxPlayerCount, game.MaxPlayersPerTeam))
			return true;
		
		if (settings.AllowSplitTeams)
		{
			// Splitting is usually only allowed for custom lobbies
			if (settings.AllowCustoms && game.AllowCustomLobbies && CanFitTotal(playerCount, game.BaseMaxPlayerCount, game.MaxTeamCount, game.MaxPlayersPerTeam))
				return true;

			// Here we check if the game can be played with mods
			if (settings.AllowMods && game is { SupportsModsForMorePlayers: true, ModdedMaxPlayerCount: { } moddedMax } && CanFitTotal(playerCount, moddedMax, game.MaxTeamCount, game.MaxPlayersPerTeam))
				return true;

			return false;
		}

		if (settings.AllowCustoms && game.AllowCustomLobbies && CanFitTogether(playerCount, game.BaseMaxPlayerCount, game.MaxPlayersPerTeam))
			return true;

		if (settings.AllowMods && game is { SupportsModsForMorePlayers: true, ModdedMaxPlayerCount: { } moddedTogetherMax } && CanFitTogether(playerCount, moddedTogetherMax, game.MaxPlayersPerTeam))
			return true;

		return false;
	}

	private static bool CanFitTotal(int playerCount, uint totalCapacity, uint? maxTeamCount, uint? maxPlayersPerTeam)
	{
		if (maxTeamCount is { } teams && maxPlayersPerTeam is { } playersPerTeam)
			return teams * playersPerTeam >= playerCount;

		return totalCapacity >= playerCount;
	}

	private static bool CanFitTogether(int playerCount, uint totalCapacity, uint? maxPlayersPerTeam)
	{
		if (maxPlayersPerTeam is { } playersPerTeam)
			return playersPerTeam >= playerCount;

		return totalCapacity >= playerCount;
	}
}
