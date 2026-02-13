using Frogobot.Core.Utils;

namespace Frogobot.Tests;

public class EmojiUtilsTest
{
	[Test]
	public void EnsureEmojiDataIsValid()
	{
		var (name, id) = EmojiUtils.GetEmojiDataFromName("<:emoji_name:1234567890>");
        
		using (Assert.EnterMultipleScope())
        {
            Assert.That(name, Is.EqualTo("emoji_name"));
            Assert.That(id, Is.EqualTo(1234567890));
        }
    }
	
	[Test]
	public void EnsureEmojiDataIsValid_ThrowsExceptionForInvalidEmojiName()
	{
		Assert.Throws<ArgumentException>(() => EmojiUtils.GetEmojiDataFromName("invalid_emoji_name"));
	}
}
