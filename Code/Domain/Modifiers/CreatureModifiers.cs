namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Modifiers;

public static class CreatureModifiers
{
    public static ICreature WithMagicShield(this ICreature creature, int charges = 1)
    {
        return new MagicShield(creature, charges);
    }

    public static ICreature WithAttackMastery(this ICreature creature, int stacks = 1)
    {
        // второе условие потребовал компилятор, хотя оно не будет пустым точно после TryFindAttackMastery
        if (TryFindAttackMastery(creature, out AttackMastery? existing) && existing is not null)
        {
            existing.AddStacks(stacks);
            return creature;
        }

        return new AttackMastery(creature, stacks);
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