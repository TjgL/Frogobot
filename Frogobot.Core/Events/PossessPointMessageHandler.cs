using Frogobot.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;

namespace Frogobot.Core.Events;

public class PossessPointMessageHandler : IMessageCreateGatewayHandler
{
	private readonly IServiceScopeFactory _scopeFactory;
	private readonly DiscordEmojiOptions _emojiOptions;

	public PossessPointMessageHandler(IServiceScopeFactory scopeFactory, IOptions<DiscordEmojiOptions> emojiOptions)
	{
		_scopeFactory = scopeFactory;
		_emojiOptions = emojiOptions.Value;
	}

	public async ValueTask HandleAsync(Message message)
	{
		Console.WriteLine($"Message received from {message.Author.Username}");
		
		var result = MessageContainsMagicWord(message.Content);
		
		if (!result.HasMatch) 
			return;
		
		using var scope = _scopeFactory.CreateScope();
		var possessPointService = scope.ServiceProvider.GetRequiredService<IPossessPointService>();

		if (result.ContainsPossessSlime)
		{
			await message.AddReactionAsync(_emojiOptions.GetReactionEmojiFrom(_emojiOptions.PossessPoint));
			await possessPointService.AddPossessPointAsync(message.Author.Id);
		}
		else if (result.ContainsSlime)
		{
			await message.AddReactionAsync(_emojiOptions.GetReactionEmojiFrom(_emojiOptions.Slime));
		}
		
		if (result.ContainsAnakin)
		{
			await message.AddReactionAsync(_emojiOptions.GetReactionEmojiFrom(_emojiOptions.Anakin));
		}
	}

	public MessageResult MessageContainsMagicWord(string message)
	{
		var msg = message.ToLowerInvariant();
		var result = new MessageResult();

		if (msg.Contains("possesslime"))
		{
			result.ContainsPossessSlime = true;
		}
		else if (msg.Contains("slime"))
		{
			result.ContainsSlime = true;
		}

		if (msg.Contains("anakin"))
		{
			result.ContainsAnakin = true;
		}

		return result;
	}

	public struct MessageResult
	{
		public bool HasMatch => ContainsSlime || ContainsPossessSlime || ContainsAnakin;
		
		public bool ContainsSlime { get; set; }
		public bool ContainsPossessSlime { get; set; }
		public bool ContainsAnakin { get; set; }
	}
}
