using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

public static class CreatureModifiers
{
    public static ICreature WithMagicShield(this ICreature creature, int charges = 1)
    {
        return new MagicShieldApply(charges).Apply(creature);
    }

    public static ICreature WithAttackMastery(this ICreature creature, int stacks = 1)
    {
        return new AttackMasteryApply(stacks).Apply(creature);
    }
}