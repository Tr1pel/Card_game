using Itmo.ObjectOrientedProgramming.Lab3.Battle;
using Itmo.ObjectOrientedProgramming.Lab3.Context.Board;
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

        ResultType outcome = engine.Fight(
            player1Board: new PlayerBoard(),
            player2Board: new PlayerBoard());

        Assert.Equal(ResultType.Draw, outcome);
    }

    [Fact]
    public void Fight_Player1HasAttacker_Player2Empty_ShouldReturnPlayer1Win()
    {
        var engine = new BattleEngine(new ZeroRng());

        var board1 = new PlayerBoard();
        var board2 = new PlayerBoard();
        board1.AddFromBoard(new DummyCreature(3, 3));

        ResultType outcome = engine.Fight(
            player1Board: board1,
            player2Board: board2);

        Assert.Equal(ResultType.Player1Win, outcome);
    }

    [Fact]
    public void Fight_Player2HasAttacker_Player1Empty_ShouldReturnPlayer2Win()
    {
        var engine = new BattleEngine(new ZeroRng());

        var board1 = new PlayerBoard();
        var board2 = new PlayerBoard();
        board2.AddFromBoard(new DummyCreature(2, 2));

        ResultType outcome = engine.Fight(
            player1Board: board1,
            player2Board: board2);

        Assert.Equal(ResultType.Player2Win, outcome);
    }

    [Fact]
    public void Fight_StrongerAttackerShouldKillAndWin()
    {
        var engine = new BattleEngine(new ZeroRng());

        var board1 = new PlayerBoard();
        var board2 = new PlayerBoard();
        board1.AddFromBoard(new DummyCreature(3, 3));
        board2.AddFromBoard(new DummyCreature(1, 2));

        IBoard p1 = board1;
        IBoard p2 = board2;

        ResultType outcome = engine.Fight(p1, p2);

        Assert.Equal(ResultType.Player1Win, outcome);
    }

    [Fact]
    public void Fight_AttackerWithZeroAttack_ShouldNotBeChosen_AndResultDraw()
    {
        var engine = new BattleEngine(new ZeroRng());

        var board1 = new PlayerBoard();
        var board2 = new PlayerBoard();
        board1.AddFromBoard(new DummyCreature(0, 5));
        board2.AddFromBoard(new DummyCreature(0, 5));

        IBoard p1 = board1;
        IBoard p2 = board2;

        ResultType outcome = engine.Fight(p1, p2);

        Assert.Equal(ResultType.Draw, outcome);
    }
}