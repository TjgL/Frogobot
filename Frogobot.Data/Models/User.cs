using System.ComponentModel.DataAnnotations;

namespace Frogobot.Data.Models;

public class User
{
	[Key]
	public ulong DiscordUserId { get; init; }
	
	public int PossessPoints { get; set; } = 0;
}
