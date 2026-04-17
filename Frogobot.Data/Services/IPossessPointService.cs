using Frogobot.Data.Dto;

namespace Frogobot.Data.Services;

public interface IPossessPointService
{
	public Task<int> GetUserPossessPointsAsync(ulong userId);
	public Task<int> AddPossessPointAsync(ulong userId);
	public Task<int> IncreasePossessPointsAsync(ulong userId, int points);
	public Task<PossessScoreboardRank[]> GetPossessScoreboardAsync(int limit = 10);
}
