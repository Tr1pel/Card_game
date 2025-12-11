using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;
using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class CloneTests
{
    [Fact]
    public void Clone_ShouldBeIndependentFromOriginal()
    {
        var original = new DummyCreature(3, 4);

        ICreature clone = original.CloneForNewContext();
        clone.ModifyAttack(2);

        Assert.Equal(3, original.Attack);
        Assert.Equal(5, clone.Attack);
        Assert.NotSame(original, clone);
    }

    [Fact]
    public void Clone_WithMagicShield_ShouldResetSpentCharges()
    {
        ICreature original = new DummyCreature(2, 10).WithMagicShield(charges: 2);

        original.TakeDamage(99);
        Assert.Equal(10, original.Health);

        ICreature clone = original.CloneForNewContext();

        clone.TakeDamage(5);
        clone.TakeDamage(5);
        Assert.Equal(10, clone.Health);

        clone.TakeDamage(5);
        Assert.Equal(5, clone.Health);
    }

    [Fact]
    public void Clone_ImmortalHorror_ShouldRestoreRebornAbility()
    {
        var horror = new ImmortalHorror();
        horror.TakeDamage(10);
        Assert.True(horror.IsAlive);
        Assert.Equal(1, horror.Health);

        ICreature clone = horror.CloneForNewContext();

        clone.TakeDamage(5);
        Assert.True(clone.IsAlive);
        Assert.Equal(1, clone.Health);
    }

    [Fact]
    public void Clone_ShouldPreserveAttackMasteryStacks()
    {
        ICreature attacker = new DummyCreature(3, 5).WithAttackMastery(1);

        ICreature clone = attacker.CloneForNewContext();
        var target = new DummyCreature(1, 12);

        clone.AttackTarget(target);

        Assert.Equal(6, target.Health);
    }
}