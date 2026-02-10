using Frogobot.Data.Dto;
using Frogobot.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Frogobot.Data.Services;


public class PossessPointService : IPossessPointService
{
	private readonly FrogoContext _dbContext;
	
	public PossessPointService(FrogoContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task AddPossessPointAsync(ulong userId)
	{
		var user = await _dbContext.Users.FindAsync(userId);
		if (user is null)
		{
			user = new User { DiscordUserId = userId };
			_dbContext.Users.Add(user);
		}
		
		user.PossessPoints++;
		await _dbContext.SaveChangesAsync();
	}

	public async Task<PossessScoreboardRank[]> GetPossessScoreboardAsync(int limit = 10)
	{
		var users = await _dbContext.Users
			.OrderByDescending(u => u.PossessPoints)
			.Take(limit)
			.ToListAsync();
		
		return users.Select((user, index) => new PossessScoreboardRank
		{
			UserId = user.DiscordUserId,
			PossessPoints = user.PossessPoints,
			Rank = (uint) index + 1
		}).ToArray();
	}

	public async Task<int> GetUserPossessPointsAsync(ulong userId)
	{
		var user = await _dbContext.Users.FindAsync(userId);
		return user?.PossessPoints ?? 0;
	}
}
