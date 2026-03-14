using Frogobot.Data;
using Frogobot.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCord.Gateway;
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
		builder.Services.AddScoped<IGameFinderService, GameFinderService>();

		// Discord Bot
		builder.Services
			.AddDiscordGateway(opt =>
			{
				opt.Intents = GatewayIntents.GuildMessages
					| GatewayIntents.DirectMessages
					| GatewayIntents.MessageContent
					| GatewayIntents.DirectMessageReactions
					| GatewayIntents.GuildMessageReactions;
			})
			.AddApplicationCommands()
			.AddGatewayHandlers(typeof(Program).Assembly);
		
		var app = builder.Build();

		using (var scope = app.Services.CreateScope())
		{
			var db = scope.ServiceProvider.GetRequiredService<FrogoContext>();
			await db.Database.MigrateAsync();
		}

		app.AddModules(typeof(Program).Assembly);

		await app.RunAsync();
	}
}
