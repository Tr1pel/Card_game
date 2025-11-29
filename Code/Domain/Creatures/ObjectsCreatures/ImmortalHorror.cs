namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;

public sealed class ImmortalHorror : Creature
{
    private bool _rebornUsed;

    public ImmortalHorror() : base(attack: 4, health: 4) { }

    private ImmortalHorror(int attack, int health, bool rebornUsed) : base(attack, health)
    {
        _rebornUsed = rebornUsed;
    }

    public override void TakeDamage(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        bool wasAlive = IsAlive;
        base.TakeDamage(amount);

        if (wasAlive && !IsAlive && !_rebornUsed)
        {
            _rebornUsed = true;
            ModifyHealth(1 - Health);
        }
    }

    protected override Creature Instantiate(int attack, int health)
    {
        return new ImmortalHorror(attack, health, rebornUsed: false);
    }

    protected override void ResetEphemeralState()
    {
        _rebornUsed = false;
    }
}