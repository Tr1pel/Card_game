using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

public sealed class MagicShieldApplier : IModifier
{
    private readonly int _charges;

    public MagicShieldApplier(int charges)
    {
        _charges = charges;
    }

    public ICreature Apply(ICreature creature)
    {
        if (TryFindMagicShield(creature, out MagicShield? shield) && shield is not null)
        {
            shield.AddCharges(_charges);
            return creature;
        }

        return new MagicShield(creature, _charges);
    }

    private static bool TryFindMagicShield(ICreature creature, out MagicShield? found)
    {
        found = null;
        ICreature current = creature;

        while (current is CreatureDecorator decorator)
        {
            if (current is MagicShield shield)
            {
                found = shield;
                return true;
            }

            current = decorator.Inner;
        }

        return false;
    }
}