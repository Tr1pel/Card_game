using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;
using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;

public sealed class AmuletMasterFactory : CreatureFactory
{
    public AmuletMasterFactory() : base("Amulet Master") { }

    protected override ICreature CreateInternal()
    {
        return new AmuletMaster().WithMagicShield().WithAttackMastery();
    }
}