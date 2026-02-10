using Frogobot.Data;
using Frogobot.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;

namespace Frogobot.Core;

internal class Program
{
	public static async Task Main(string[] args)
	{
		var builder = Host.CreateApplicationBuilder(args);

		if (builder.Environment.IsDevelopment())
		{
			builder.Configuration.AddUserSecrets<Program>();
		}

		builder.Services.Configure<DiscordEmojiOptions>(builder.Configuration.GetSection(DiscordEmojiOptions.SectionName));

		// Frogobot Data
		builder.Services.AddDbContextPool<FrogoContext>(options =>
		{
			options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ??
			                  "Data Source=frogobot.db");
		});
		
		builder.Services.AddScoped<IPossessPointService, PossessPointService>();

		// Discord Bot
		builder.Services
			.AddDiscordGateway()
			.AddApplicationCommands();
		
		var app = builder.Build();

		// TODO : temp, replace by migrations later
		using (var scope = app.Services.CreateScope())
		{
			var db = scope.ServiceProvider.GetRequiredService<FrogoContext>();
			await db.Database.EnsureCreatedAsync();
		}

		app.AddModules(typeof(Program).Assembly);

		await app.RunAsync();
	}
}
