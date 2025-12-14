using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;

public sealed class MimicChestFactory : CreatureFactory
{
    public MimicChestFactory() : base("MimicChest") { }

    protected override ICreature CreateInternal()
    {
        return new MimicChest();
    }
}