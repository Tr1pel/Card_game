using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures;

public abstract class Creature : ICreature
{
    public AttackValue Attack { get; private set; }

    public HealthValue Health { get; private set; }

    public bool IsAlive => Health.Value > 0;

    protected Creature(int attack, int health)
    {
        Attack = new AttackValue(attack);
        Health = new HealthValue(health);
    }

    public virtual void AttackTarget(ICreature target)
    {
        ArgumentNullException.ThrowIfNull(target);
        if (!IsAlive)
        {
            throw new InvalidOperationException("Мёртвое существо не может атаковать");
        }

        if (!target.IsAlive)
        {
            throw new InvalidOperationException("Нельзя атаковать мертвое существо");
        }

        target.TakeDamage(Attack.Value);
    }

    public virtual void TakeDamage(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        Health = Health.Subtract(amount);
    }

    public void ModifyAttack(int delta)
    {
        Attack = Attack.Add(delta);
    }

    public void ModifyHealth(int delta)
    {
        Health = Health.Add(delta);
    }

    public ICreature CloneForNewContext()
    {
        Creature copy = Instantiate(Attack.Value, Health.Value);
        copy.ResetEphemeralState();
        return copy;
    }

    protected abstract Creature Instantiate(int attack, int health);

    protected virtual void ResetEphemeralState() { }
}