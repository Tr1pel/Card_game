namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Spells;

public static class UseSpell
{
    public static ICreature Apply(this ICreature target, ISpell spell)
    {
        return spell.Cast(target);
    }
}