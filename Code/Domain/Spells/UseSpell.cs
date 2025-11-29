using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spells;

public static class UseSpell
{
    public static ICreature Apply(this ICreature target, ISpell spell)
    {
        return spell.Cast(target);
    }
}