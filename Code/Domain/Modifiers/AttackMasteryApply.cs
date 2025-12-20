using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

public sealed class AttackMasteryApply : IModifier
{
    private readonly int _stacks;

    public AttackMasteryApply(int stacks)
    {
        _stacks = stacks;
    }

    public ICreature Apply(ICreature creature)
    {
        if (TryFindAttackMastery(creature, out AttackMastery? existing) && existing is not null)
        {
            existing.AddStacks(_stacks);
            return creature;
        }

        return new AttackMastery(creature, _stacks);
    }

    private static bool TryFindAttackMastery(ICreature creature, out AttackMastery? found)
    {
        found = null;
        ICreature current = creature;
        while (current is CreatureDecorator decorator)
        {
            if (current is AttackMastery am)
            {
                found = am;
                return true;
            }

            current = decorator.Inner;
        }

        return false;
    }
}