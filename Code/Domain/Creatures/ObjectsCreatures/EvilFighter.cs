namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;

public sealed class EvilFighter : Creature
{
    public EvilFighter() : base(attack: 1, health: 6) { }

    private EvilFighter(int attack, int health) : base(attack, health) { }

    public override void TakeDamage(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        int wasHelth = Health.Value;
        base.TakeDamage(amount);

        if (wasHelth > 0 && Health.Value > 0)
        {
            ModifyAttack(Attack.Value);
        }
    }

    protected override Creature Instantiate(int attack, int health)
    {
        return new EvilFighter(attack, health);
    }
}