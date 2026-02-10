using Frogobot.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Frogobot.Data;

public class FrogoContext : DbContext
{
	public DbSet<User> Users { get; set; }

	public FrogoContext(DbContextOptions<FrogoContext> options) : base(options)
	{
	}
}