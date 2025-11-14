namespace Itmo.ObjectOrientedProgramming.Lab3.Domain;

public abstract class Creature : ICreature
{
    public int Attack { get; private set; }

    public int Health { get; private set; }

    public bool IsAlive => Health > 0;

    protected Creature(int attack, int health)
    {
        Attack = attack;
        Health = health;
    }

    public virtual void AttackTarget(ICreature target)
    {
        ArgumentNullException.ThrowIfNull(target);
        if (!IsAlive)
        {
            throw new InvalidOperationException("Мёртвое существо не может атаковать");
        }

        if (Attack <= 0)
        {
            throw new InvalidOperationException("Существо с неположительной атакой не может атаковать");
        }

        if (!target.IsAlive)
        {
            throw new InvalidOperationException("Нельзя атаковать мертвое существо");
        }

        target.TakeDamage(Attack);
    }

    public virtual void TakeDamage(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        Health -= amount;
    }

    public void ModifyAttack(int delta)
    {
        Attack += delta;
    }

    public void ModifyHealth(int delta)
    {
        Health += delta;
    }

    public ICreature CloneForNewContext()
    {
        Creature copy = Instantiate(Attack, Health);
        copy.ResetEphemeralState();
        return copy;
    }

    protected abstract Creature Instantiate(int attack, int health);

    protected virtual void ResetEphemeralState() { }
}