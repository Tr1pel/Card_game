using Itmo.ObjectOrientedProgramming.Lab3.Battle;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class BattleTests
{
    private sealed class ZeroRng : IRng
    {
        public int NextInt(int min, int max) => min;
    }

    [Fact]
    public void Fight_WithEmptyBoards_ShouldReturnDraw()
    {
        var engine = new BattleEngine(new ZeroRng());

        BattleOutcome outcome = engine.Fight(
            player1Board: new List<ICreature>(),
            player2Board: new List<ICreature>());

        Assert.Equal(BattleOutcome.Draw, outcome);
    }

    [Fact]
    public void Fight_Player1HasAttacker_Player2Empty_ShouldReturnPlayer1Win()
    {
        var engine = new BattleEngine(new ZeroRng());

        BattleOutcome outcome = engine.Fight(
            player1Board: new List<ICreature> { new DummyCreature(3, 3) },
            player2Board: new List<ICreature>());

        Assert.Equal(BattleOutcome.Player1Win, outcome);
    }

    [Fact]
    public void Fight_Player2HasAttacker_Player1Empty_ShouldReturnPlayer2Win()
    {
        var engine = new BattleEngine(new ZeroRng());

        BattleOutcome outcome = engine.Fight(
            player1Board: new List<ICreature>(),
            player2Board: new List<ICreature> { new DummyCreature(2, 2) });

        Assert.Equal(BattleOutcome.Player2Win, outcome);
    }

    [Fact]
    public void Fight_StrongerAttackerShouldKillAndWin()
    {
        var engine = new BattleEngine(new ZeroRng());

        var p1 = new List<ICreature> { new DummyCreature(3, 3) };
        var p2 = new List<ICreature> { new DummyCreature(1, 2) };

        BattleOutcome outcome = engine.Fight(p1, p2);

        Assert.Equal(BattleOutcome.Player1Win, outcome);
    }

    [Fact]
    public void Fight_AttackerWithZeroAttack_ShouldNotBeChosen_AndResultDraw()
    {
        var engine = new BattleEngine(new ZeroRng());

        var p1 = new List<ICreature> { new DummyCreature(0, 5) };
        var p2 = new List<ICreature> { new DummyCreature(0, 5) };

        BattleOutcome outcome = engine.Fight(p1, p2);

        Assert.Equal(BattleOutcome.Draw, outcome);
    }
}