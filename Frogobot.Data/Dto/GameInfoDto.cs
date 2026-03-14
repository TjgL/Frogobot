namespace Frogobot.Data.Dto;

public record RegisterGameOption(
	string GameName,
	uint BaseMaxPlayerCount,
	bool AllowCustomLobbies = false,
	uint? MaxTeamCount = null,
	uint? MaxPlayersPerTeam = null,
	bool SupportsModsForMorePlayers = false,
	uint? ModdedMaxPlayerCount = null);

public record GameSearchSettings(
	ulong[] UserIds,
	bool AllowCustoms = false,
	bool AllowSplitTeams = false,
	bool AllowMods = false);

public record GameInfo(
	string GameName,
	uint BaseMaxPlayerCount,
	bool AllowCustomLobbies,
	uint? MaxTeamCount,
	uint? MaxPlayersPerTeam,
	bool SupportsModsForMorePlayers,
	uint? ModdedMaxPlayerCount);

public record SharedGamesInfo(
	ulong[] DiscordUserId,
	ICollection<GameInfo> Games);
