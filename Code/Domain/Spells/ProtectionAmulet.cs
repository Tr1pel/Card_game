using Itmo.ObjectOrientedProgramming.Lab3.Domain.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Spells;

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