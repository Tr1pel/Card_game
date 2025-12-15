using Itmo.ObjectOrientedProgramming.Lab3.Battle;
using Itmo.ObjectOrientedProgramming.Lab3.Context.Board;
using Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;
using Itmo.ObjectOrientedProgramming.Lab3.Spells;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class GameScenarioTests
{
    [Fact]
    public void GameScenario_WithSystemRandom_ShouldRunAndProduceOutcome()
    {
        ICatalog catalog = new PlayerCatalog().AddPrototypes();
        var rng = new SystemRandom();

        var board1 = new PlayerBoard();
        var board2 = new PlayerBoard();

        var factories = catalog.Factories.ToList();
        board1.AddFromBoard(catalog.Create(factories[0].Id));
        board1.AddFromBoard(catalog.Create(factories[1].Id));
        board1.AddFromBoard(catalog.Create(factories[2].Id));
        board1.AddFromBoard(catalog.Create(factories[3].Id));

        var spells = new ISpell[]
        {
            new PowerPotion(),
            new EndurancePotion(),
            new ProtectionAmulet(),
            new MagicMirror(),
        };

        var allTargets = board1.Creatures.Concat(board2.Creatures).ToList();
        foreach (ISpell spell in spells)
        {
            int idx = rng.NextInt(0, allTargets.Count);
            allTargets[idx].Apply(spell);
        }

        var engine = new BattleEngine(rng);
        ResultType outcome = engine.Fight(board1, board2);

        Assert.True(Enum.IsDefined(outcome));
    }
}