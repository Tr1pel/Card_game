using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public interface ICreatureBuilder
{
    ICreatureBuilder WithAttack(int delta);

    ICreatureBuilder WithHealth(int delta);

    ICreatureBuilder WithModifier(Func<ICreature, ICreature> modifier);

    ICreature Build();
}