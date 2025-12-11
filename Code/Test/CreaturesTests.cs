using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;
using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class CreaturesTests
{
    [Fact]
    public void BattleAnalyst_ShouldBuffAttackBeforeStrike()
    {
        var attacker = new BattleAnalyst(); // 2/4
        var target = new DummyCreature(1, 10);

        attacker.AttackTarget(target);

        Assert.Equal(4, attacker.Attack);
        Assert.Equal(6, target.Health);
    }

    [Fact]
    public void EvilFighter_ShouldDoubleAttackOnNonLethalDamage()
    {
        var fighter = new EvilFighter(); // 1/6

        fighter.TakeDamage(2);

        Assert.Equal(4, fighter.Health);
        Assert.Equal(2, fighter.Attack);
    }

    [Fact]
    public void EvilFighter_ShouldNotDoubleAttackOnLethalDamage()
    {
        var fighter = new EvilFighter(); // 1/6

        fighter.TakeDamage(10);

        Assert.False(fighter.IsAlive);
        Assert.Equal(1, fighter.Attack);
    }

    [Fact]
    public void MimicChest_ShouldCopyMaxAttackAndHealthBeforeAttack()
    {
        var mimic = new MimicChest(); // 1/1
        var target = new DummyCreature(5, 2);

        mimic.AttackTarget(target);

        Assert.Equal(5, mimic.Attack);
        Assert.Equal(2, mimic.Health);
        Assert.False(target.IsAlive);
    }

    [Fact]
    public void ImmortalHorror_ShouldRebornOnce()
    {
        var horror = new ImmortalHorror(); // 4/4

        horror.TakeDamage(5);

        Assert.True(horror.IsAlive);
        Assert.Equal(4, horror.Attack);
        Assert.Equal(1, horror.Health);

        horror.TakeDamage(2);

        Assert.False(horror.IsAlive);
        Assert.Equal(-1, horror.Health);
    }

    [Fact]
    public void AmuletMaster_WithPresetModifiers_ShouldShieldThenDoubleAttack()
    {
        ICreature amuletMaster = new AmuletMaster() // 5/2
            .WithMagicShield(charges: 1)
            .WithAttackMastery(stacks: 1);

        amuletMaster.TakeDamage(10);
        Assert.True(amuletMaster.IsAlive);
        Assert.Equal(2, amuletMaster.Health);

        amuletMaster.TakeDamage(1);
        Assert.Equal(1, amuletMaster.Health);
    }
}