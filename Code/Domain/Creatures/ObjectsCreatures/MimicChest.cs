namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;

public sealed class MimicChest : Creature
{
    public MimicChest() : base(attack: 1, health: 1) { }

    private MimicChest(int attack, int health) : base(attack, health) { }

    public override void AttackTarget(ICreature target)
    {
        int newAttack = Math.Max(Attack, target.Attack);
        int newHealth = Math.Max(Health, target.Health);

        int dA = newAttack - Attack;
        int dH = newHealth - Health;
        if (dA != 0)
        {
            ModifyAttack(dA);
        }

        if (dH != 0)
        {
            ModifyHealth(dH);
        }

        base.AttackTarget(target);
    }

    protected override Creature Instantiate(int attack, int health)
    {
        return new MimicChest(attack, health);
    }
}