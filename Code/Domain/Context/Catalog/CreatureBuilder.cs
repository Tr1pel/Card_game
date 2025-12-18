using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public sealed class CreatureBuilder : ICreatureBuilder
{
    private readonly Func<ICreature> _factory;
    private readonly List<Func<ICreature, ICreature>> _modifiers = new();
    private int _attackD;
    private int _healthD;

    public CreatureBuilder(Func<ICreature> factory)
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

    public ICreatureBuilder WithModifier(Func<ICreature, ICreature> modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    public ICreature Build()
    {
        ICreature creature = _factory();

        if (_attackD != 0)
        {
            creature.ModifyAttack(_attackD);
        }

        if (_healthD != 0)
        {
            creature.ModifyHealth(_healthD);
        }

        foreach (Func<ICreature, ICreature> modifier in _modifiers)
        {
            creature = modifier(creature);
        }

        return creature;
    }
}