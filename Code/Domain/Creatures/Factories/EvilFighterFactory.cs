using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;

public sealed class EvilFighterFactory : CreatureFactory
{
    public EvilFighterFactory() : base("EvilFighter") { }

    protected override ICreature CreateInternal()
    {
        return new EvilFighter();
    }
}