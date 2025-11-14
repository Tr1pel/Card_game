namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Creatures;

public sealed class BattleAnalyst : Creature
{
    public BattleAnalyst() : base(attack: 2, health: 4) { }

    private BattleAnalyst(int attack, int health) : base(attack, health) { }

    public override void AttackTarget(ICreature target)
    {
        ModifyAttack(+2);
        base.AttackTarget(target);
    }

    protected override Creature Instantiate(int attack, int health)
    {
        return new BattleAnalyst(attack, health);
    }
}