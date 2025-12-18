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

        Assert.Equal(4, attacker.Attack.Value);
        Assert.Equal(6, target.Health.Value);
    }

    [Fact]
    public void EvilFighter_ShouldDoubleAttackOnNonLethalDamage()
    {
        var fighter = new EvilFighter(); // 1/6

        fighter.TakeDamage(2);

        Assert.Equal(4, fighter.Health.Value);
        Assert.Equal(2, fighter.Attack.Value);
    }

    [Fact]
    public void EvilFighter_ShouldNotDoubleAttackOnLethalDamage()
    {
        var fighter = new EvilFighter(); // 1/6

        fighter.TakeDamage(10);

        Assert.False(fighter.IsAlive);
        Assert.Equal(1, fighter.Attack.Value);
    }

    [Fact]
    public void MimicChest_ShouldCopyMaxAttackAndHealthBeforeAttack()
    {
        var mimic = new MimicChest(); // 1/1
        var target = new DummyCreature(5, 2);

        mimic.AttackTarget(target);

        Assert.Equal(5, mimic.Attack.Value);
        Assert.Equal(2, mimic.Health.Value);
        Assert.False(target.IsAlive);
    }

    [Fact]
    public void ImmortalHorror_ShouldRebornOnce()
    {
        var horror = new ImmortalHorror(); // 4/4

        horror.TakeDamage(5);

        Assert.True(horror.IsAlive);
        Assert.Equal(4, horror.Attack.Value);
        Assert.Equal(1, horror.Health.Value);

        horror.TakeDamage(2);

        Assert.False(horror.IsAlive);
        Assert.Equal(-1, horror.Health.Value);
    }

    [Fact]
    public void AmuletMaster_WithPresetModifiers_ShouldShieldThenDoubleAttack()
    {
        ICreature amuletMaster = new AmuletMaster() // 5/2
            .WithMagicShield(charges: 1)
            .WithAttackMastery(stacks: 1);

        amuletMaster.TakeDamage(10);
        Assert.True(amuletMaster.IsAlive);
        Assert.Equal(2, amuletMaster.Health.Value);

        amuletMaster.TakeDamage(1);
        Assert.Equal(1, amuletMaster.Health.Value);
    }

    public sealed class DummyCreature : Creature
    {
        public DummyCreature(int attack, int health) : base(attack, health) { }

        protected override Creature Instantiate(int attack, int health)
        {
            return new DummyCreature(attack, health);
        }
    }
}