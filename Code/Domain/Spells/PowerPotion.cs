namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Spells;

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