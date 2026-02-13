using Frogobot.Core.Events;

namespace Frogobot.Tests;

public class MagicWordTest
{
	[Test]
	public void CheckMagicWordHasAll()
	{
		const string message = "PossesSlime contains a slime named Anakin";
		
		var result = PossessPointMessageHandler.MessageContainsMagicWord(message);
		Assert.That(result.ContainsPossessSlime, Is.True);
		Assert.That(result.ContainsSlime, Is.False);
		Assert.That(result.ContainsAnakin, Is.True);
	}

	[Test]
	public void CheckMagicWordTypo()
	{
		string message = "Posseslime has a typo";
		
		var result = PossessPointMessageHandler.MessageContainsMagicWord(message);
		Assert.That(result.ContainsPossessSlime, Is.True);
		Assert.That(result.ContainsSlime, Is.False);
		Assert.That(result.ContainsAnakin, Is.False);
		
		message = "Possessslime with extra S";
		result = PossessPointMessageHandler.MessageContainsMagicWord(message);
		Assert.That(result.ContainsPossessSlime, Is.True);
		Assert.That(result.ContainsSlime, Is.False);
		Assert.That(result.ContainsAnakin, Is.False);
	}
}
