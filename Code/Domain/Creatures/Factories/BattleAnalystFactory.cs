using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;

public sealed class BattleAnalystFactory : CreatureFactory
{
    public BattleAnalystFactory() : base("BattleAnalyst") { }

    protected override ICreature CreateInternal()
    {
        return new BattleAnalyst();
    }
}