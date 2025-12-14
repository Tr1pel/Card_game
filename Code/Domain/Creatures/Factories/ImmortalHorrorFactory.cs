using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;

public sealed class ImmortalHorrorFactory : CreatureFactory
{
    public ImmortalHorrorFactory() : base("Immortal Horror") { }

    protected override ICreature CreateInternal()
    {
        return new ImmortalHorror();
    }
}