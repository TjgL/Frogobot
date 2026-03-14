using Frogobot.Data.Dto;

namespace Frogobot.Data.Services;

public interface IGameFinderService
{
	public ValueTask RegisterGameAsync(RegisterGameOption option);
	public ValueTask<bool> SetUserAsOwnerAsync(ulong userId, string game);

	public Task<SharedGamesInfo> GetSharedGamesAsync(GameSearchSettings settings);
	
	public Task<GameInfo[]> GetOwnedGamesAsync(ulong userId);
}
