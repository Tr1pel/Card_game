namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Spells;

public sealed class EndurancePotion : ISpell
{
    private readonly int _amount;

    public EndurancePotion(int amount = 5)
    {
        _amount = amount;
    }

    public ICreature Cast(ICreature target)
    {
        target.ModifyHealth(_amount);
        return target;
    }
}