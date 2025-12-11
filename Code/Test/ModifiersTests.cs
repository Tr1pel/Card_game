using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;
using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class ModifiersTests
{
    [Fact]
    public void MagicShield_ShouldIgnoreDamageUntilChargesSpent()
    {
        ICreature creature = new DummyCreature(2, 10).WithMagicShield(2);

        creature.TakeDamage(5);
        creature.TakeDamage(1);

        Assert.Equal(10, creature.Health);

        creature.TakeDamage(4);

        Assert.Equal(6, creature.Health);
    }

    [Fact]
    public void MagicShield_ShouldAccumulateChargesWhenReapplied()
    {
        ICreature creature = new DummyCreature(1, 5).WithMagicShield(charges: 1);

        ICreature same = creature.WithMagicShield(2);

        Assert.Same(creature, same);

        creature.TakeDamage(1);
        creature.TakeDamage(1);
        creature.TakeDamage(1);

        Assert.Equal(5, creature.Health);

        creature.TakeDamage(2);

        Assert.Equal(3, creature.Health);
    }

    [Fact]
    public void MagicShieldClone_ShouldResetSpentCharges()
    {
        ICreature creature = new DummyCreature(1, 5).WithMagicShield(1);
        creature.TakeDamage(10);

        ICreature clone = creature.CloneForNewContext();

        clone.TakeDamage(10);

        Assert.Equal(5, clone.Health);
    }

    [Fact]
    public void AttackMastery_ShouldPerformExtraAttacks()
    {
        ICreature attacker = new DummyCreature(2, 5).WithAttackMastery(1);
        var target = new DummyCreature(1, 5);

        attacker.AttackTarget(target);

        Assert.Equal(1, target.Health);
    }

    [Fact]
    public void AttackMastery_ShouldAccumulateStacksWhenReapplied()
    {
        ICreature attacker = new DummyCreature(2, 5).WithAttackMastery(1);
        ICreature same = attacker.WithAttackMastery(2);

        Assert.Same(attacker, same);

        var target = new DummyCreature(1, 10);
        attacker.AttackTarget(target);

        Assert.Equal(2, target.Health);
    }

    [Fact]
    public void AttackMastery_ShouldStopWhenTargetDies()
    {
        ICreature attacker = new DummyCreature(3, 5).WithAttackMastery(5);
        var target = new DummyCreature(0, 5);

        attacker.AttackTarget(target);

        Assert.False(target.IsAlive);
    }
}