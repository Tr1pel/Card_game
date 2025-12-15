using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

public abstract class CreatureDecorator : ICreature
{
    protected CreatureDecorator(ICreature inner)
    {
        Inner = inner;
    }

    public ICreature Inner { get; }

    public AttackValue Attack => Inner.Attack;

    public HealthValue Health => Inner.Health;

    public bool IsAlive => Inner.IsAlive;

    public virtual void AttackTarget(ICreature target)
    {
        Inner.AttackTarget(target);
    }

    public virtual void TakeDamage(int amount)
    {
        Inner.TakeDamage(amount);
    }

    public void ModifyAttack(int delta)
    {
        Inner.ModifyAttack(delta);
    }

    public void ModifyHealth(int delta)
    {
        Inner.ModifyHealth(delta);
    }

    public ICreature CloneForNewContext()
    {
        ICreature innerClone = Inner.CloneForNewContext();
        CreatureDecorator copy = Instantiate(innerClone);
        copy.ResetEphemeralState();
        return copy;
    }

    protected abstract CreatureDecorator Instantiate(ICreature inner);

    protected virtual void ResetEphemeralState() { }
}