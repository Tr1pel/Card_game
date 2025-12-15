using Itmo.ObjectOrientedProgramming.Lab3.Context.Board;
using Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;
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
    public void Catalog_Create_ShouldProduceIndependentInstances()
    {
        var catalog = new PlayerCatalog();
        var factory = new MimicChestFactory();
        catalog.AddFactory(factory);

        ICreature p1 = catalog.Create(factory.Id);
        ICreature p2 = catalog.Create(factory.Id);

        p2.TakeDamage(10);

        Assert.Equal(1, p1.Attack.Value);
        Assert.NotSame(p1, p2);
    }

    [Fact]
    public void Catalog_AddAndRemove_ShouldUpdatePrototypes()
    {
        var catalog = new PlayerCatalog();
        var p1 = new BattleAnalystFactory();
        var p2 = new EvilFighterFactory();

        catalog.AddFactory(p1);
        catalog.AddFactory(p2);
        catalog.RemoveFactory(p1);

        Assert.Single(catalog.Factories);
        Assert.Same(p2, catalog.Factories.First());
    }

    [Fact]
    public void Catalog_Create_WithUnknownId_ShouldThrow()
    {
        var catalog = new PlayerCatalog();

        Assert.Throws<InvalidOperationException>(() => catalog.Create("MissingFactory"));
    }
}