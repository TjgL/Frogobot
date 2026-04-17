using Frogobot.Core.Utils;

namespace Frogobot.Tests;

public class RouletteServiceTest
{
    [Test]
    public void TestRouletteRedWin()
    {
        // Seed 23 -> Rolls 27 (Red)
        var random = new Random(23);
        var rouletteService = new RouletteService(random);
        
        var result = rouletteService.Play(100, RouletteBetType.Red);
        
        Assert.That(result.RolledNumber, Is.EqualTo(27));
        Assert.That(result.IsWin, Is.True);
        Assert.That(result.Delta, Is.EqualTo(100));
    }

    [Test]
    public void TestRouletteRedLoss()
    {
        // Seed 0 -> Rolls 26 (Black)
        var random = new Random(0);
        var rouletteService = new RouletteService(random);
        
        var result = rouletteService.Play(100, RouletteBetType.Red);
        
        Assert.That(result.RolledNumber, Is.EqualTo(26));
        Assert.That(result.IsWin, Is.False);
        Assert.That(result.Delta, Is.EqualTo(-100));
    }

    [Test]
    public void TestRouletteNumberWin()
    {
        // Seed 23 -> Rolls 27
        var random = new Random(23);
        var rouletteService = new RouletteService(random);
        
        var result = rouletteService.Play(10, RouletteBetType.Number, 27);
        
        Assert.That(result.IsWin, Is.True);
        Assert.That(result.Delta, Is.EqualTo(350)); // 10 * (36 - 1) = 350
    }

    [Test]
    public void TestRouletteNumberLoss()
    {
        // Seed 23 -> Rolls 27
        var random = new Random(23);
        var rouletteService = new RouletteService(random);
        
        var result = rouletteService.Play(10, RouletteBetType.Number, 5);
        
        Assert.That(result.IsWin, Is.False);
        Assert.That(result.Delta, Is.EqualTo(-10));
    }

    [Test]
    public void TestRouletteZeroLossForRed()
    {
        // Seed 35 -> Rolls 0
        var random = new Random(35);
        var rouletteService = new RouletteService(random);
        
        var result = rouletteService.Play(100, RouletteBetType.Red);
        
        Assert.That(result.RolledNumber, Is.EqualTo(0));
        Assert.That(result.IsWin, Is.False);
        Assert.That(result.Delta, Is.EqualTo(-100));
    }
}
