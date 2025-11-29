using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spells;

public sealed class ProtectionAmulet : ISpell
{
    private readonly int _charges;

    public ProtectionAmulet(int charges = 1)
    {
        _charges = charges;
    }

    public ICreature Cast(ICreature target)
    {
        return target.WithMagicShield(_charges);
    }
}