using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public interface ICreatureBuilder
{
    ICreatureBuilder WithAttack(int delta);

    ICreatureBuilder WithHealth(int delta);

    ICreatureBuilder WithModifier(IModifier modifier);

    ICreature Build();
}