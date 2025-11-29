using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

public sealed class MagicShield : CreatureDecorator
{
    private int _charges;
    private int _spent;

    public MagicShield(ICreature inner, int charges = 1) : base(inner)
    {
        _charges = charges;
    }

    // public int Charges => _charges;
    public void AddCharges(int delta)
    {
        _charges += delta;
    }

    public override void TakeDamage(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        if (_spent < _charges)
        {
            _spent += 1;
            return;
        }

        base.TakeDamage(amount);
    }

    protected override CreatureDecorator Instantiate(ICreature inner)
    {
        return new MagicShield(inner, _charges);
    }

    protected override void ResetEphemeralState()
    {
        _spent = 0;
    }
}