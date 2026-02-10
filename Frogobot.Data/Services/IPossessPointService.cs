using Frogobot.Data.Dto;

namespace Frogobot.Data.Services;

public interface IPossessPointService
{
	public Task<int> GetUserPossessPointsAsync(ulong userId);
	public Task AddPossessPointAsync(ulong userId);
	public Task<PossessScoreboardRank[]> GetPossessScoreboardAsync(int limit = 10);
}
