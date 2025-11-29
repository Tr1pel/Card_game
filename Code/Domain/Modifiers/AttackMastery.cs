using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

public sealed class AttackMastery : CreatureDecorator
{
    private int _stacks;

    public AttackMastery(ICreature inner, int stacks) : base(inner)
    {
        _stacks = stacks;
    }

    public void AddStacks(int delta)
    {
        _stacks += delta;
    }

    public override void AttackTarget(ICreature target)
    {
        int totalAttacks = 1 + _stacks;

        for (int i = 0; i < totalAttacks; i++)
        {
            if (!IsAlive || !target.IsAlive)
            {
                break;
            }

            base.AttackTarget(target);
        }
    }

    protected override CreatureDecorator Instantiate(ICreature inner)
    {
        return new AttackMastery(inner, _stacks);
    }
}