namespace Frogobot.Data.Dto;

public readonly struct PossessScoreboardRank
{
	public ulong UserId { get; init; }
	public int PossessPoints { get; init; }
	public uint Rank { get; init; }
}
