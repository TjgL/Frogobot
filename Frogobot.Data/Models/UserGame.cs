using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Frogobot.Data.Models;

[PrimaryKey(nameof(DiscordUserId), nameof(GameId))]
public class UserGame
{
	public ulong DiscordUserId { get; init; }
	public uint GameId { get; init; }

	[ForeignKey(nameof(DiscordUserId))]
	public User User { get; set; } = null!;

	[ForeignKey(nameof(GameId))]
	public Game Game { get; set; } = null!;
}
