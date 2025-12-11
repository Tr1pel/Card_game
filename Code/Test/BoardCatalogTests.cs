using Itmo.ObjectOrientedProgramming.Lab3.Context.Board;
using Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class BoardCatalogTests
{
    [Fact]
    public void Board_ShouldRespectMaxSlots()
    {
        var board = new PlayerBoard(1);
        var c1 = new DummyCreature(1, 1);
        var c2 = new DummyCreature(1, 1);

        board.AddFromBoard(c1);
        board.AddFromBoard(c2);

        Assert.Single(board.Creatures);
        Assert.Same(c1, board.Creatures[0]);
    }

    [Fact]
    public void Board_RemoveFromBoard_ShouldRemoveExisting()
    {
        var board = new PlayerBoard();
        var c1 = new DummyCreature(1, 1);

        board.AddFromBoard(c1);
        board.RemoveToBoard(c1);

        Assert.Empty(board.Creatures);
    }

    [Fact]
    public void Board_CleanDead_ShouldDropDeadCreatures()
    {
        var board = new PlayerBoard();
        var alive = new DummyCreature(1, 2);
        var dead = new DummyCreature(1, 1);

        board.AddFromBoard(alive);
        board.AddFromBoard(dead);

        dead.TakeDamage(5);
        board.CleanDead();

        Assert.Single(board.Creatures);
        Assert.Same(alive, board.Creatures[0]);
    }

    [Fact]
    public void Board_GetPotentialAttackers_ShouldReturnOnlyAlive()
    {
        var board = new PlayerBoard();
        var alive = new DummyCreature(1, 2);
        var dead = new DummyCreature(1, 1);
        dead.TakeDamage(10);

        board.AddFromBoard(alive);
        board.AddFromBoard(dead);

        IReadOnlyCollection<ICreature> attackers = board.GetPotentialAttackers().ToList();

        Assert.Single(attackers);
        Assert.Same(alive, attackers.First());
    }

    [Fact]
    public void Catalog_CreateForBoard_ShouldClonePrototype()
    {
        var catalog = new PlayerCatalog();
        var prototype = new DummyCreature(2, 3);
        catalog.AddToCatalog(prototype);

        ICreature boardCopy = catalog.CreateForBoard(prototype);

        boardCopy.ModifyAttack(5);

        Assert.Equal(2, prototype.Attack);
        Assert.NotSame(prototype, boardCopy);
    }

    [Fact]
    public void Catalog_AddAndRemove_ShouldUpdatePrototypes()
    {
        var catalog = new PlayerCatalog();
        var p1 = new DummyCreature(1, 1);
        var p2 = new DummyCreature(2, 2);

        catalog.AddToCatalog(p1);
        catalog.AddToCatalog(p2);
        catalog.RemoveFromCatalog(p1);

        Assert.Single(catalog.Prototypes);
        Assert.Same(p2, catalog.Prototypes.First());
    }
}