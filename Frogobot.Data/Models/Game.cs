using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frogobot.Data.Models;

public class Game
{
	/// <summary>
	/// The unique identifier for the game.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public uint GameId { get; init; }
	
	/// <summary>
	/// The name of the game.
	/// </summary>
	[Required]
	public required string GameName { get; init; }
	
	/// <summary>
	/// The maximum number of players in the base game's standard mode (without custom lobbies or mods).
	/// For PvP games, this is the total number of players in the session.
	/// </summary>
	public uint BaseMaxPlayerCount { get; set; }
	
	/// <summary> Indicates whether the game supports custom lobbies. </summary>
	public bool AllowCustomLobbies { get; set; }

	/// <summary>
	/// The maximum number of teams allowed in the game's standard mode.
	/// </summary>
	public uint? MaxTeamCount { get; set; }

	/// <summary>
	/// The maximum number of players allowed per team.
	/// </summary>
	public uint? MaxPlayersPerTeam { get; set; }

	/// <summary>
	/// Indicates whether the game supports mods or console commands to increase the maximum player count.
	/// </summary>
	public bool SupportsModsForMorePlayers { get; set; }

	/// <summary>
	/// The maximum number of players achievable by using mods or console commands.
	/// </summary>
	public uint? ModdedMaxPlayerCount { get; set; }
}