using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spells;

public sealed class PowerPotion : ISpell
{
    private readonly int _amount;

    public PowerPotion(int amount = 5)
    {
        _amount = amount;
    }

    public ICreature Cast(ICreature target)
    {
        target.ModifyAttack(_amount);
        return target;
    }
}