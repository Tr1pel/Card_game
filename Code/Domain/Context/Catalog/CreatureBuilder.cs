using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;
using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public sealed class CreatureBuilder : ICreatureBuilder
{
    private readonly ICreatureFactory _factory;
    private readonly List<IModifier> _modifiers = new();
    private int _attackD;
    private int _healthD;

    public CreatureBuilder(ICreatureFactory factory)
    {
        _factory = factory;
    }

    public ICreatureBuilder WithAttack(int delta)
    {
        _attackD += delta;
        return this;
    }

    public ICreatureBuilder WithHealth(int delta)
    {
        _healthD += delta;
        return this;
    }

    public ICreatureBuilder WithModifier(IModifier modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    public ICreature Build()
    {
        ICreature creature = _factory.Create();

        if (_attackD != 0)
        {
            creature.ModifyAttack(_attackD);
        }

        if (_healthD != 0)
        {
            creature.ModifyHealth(_healthD);
        }

        foreach (IModifier modifier in _modifiers)
        {
            creature = modifier.Apply(creature);
        }

        return creature;
    }
}