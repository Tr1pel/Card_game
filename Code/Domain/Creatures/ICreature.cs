using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ValueObjects;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures;

public interface ICreature
{
    AttackValue Attack { get; }

    HealthValue Health { get; }

    bool IsAlive { get; }

    void AttackTarget(ICreature target);

    void TakeDamage(int amount);

    void ModifyAttack(int delta);

    void ModifyHealth(int delta);

    ICreature CloneForNewContext();
}