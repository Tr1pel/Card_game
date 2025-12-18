using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Spells;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class SpellsTests
{
    [Fact]
    public void PowerPotion_ShouldIncreaseAttackByDefaultAmount()
    {
        var creature = new DummyCreature(1, 5);
        var spell = new PowerPotion();

        ICreature result = spell.Cast(creature);

        Assert.Same(creature, result);
        Assert.Equal(6, creature.Attack.Value);
        Assert.Equal(5, creature.Health.Value);
    }

    [Fact]
    public void EndurancePotion_ShouldIncreaseHealthByDefaultAmount()
    {
        var creature = new DummyCreature(2, 3);
        var spell = new EndurancePotion();

        spell.Cast(creature);

        Assert.Equal(2, creature.Attack.Value);
        Assert.Equal(8, creature.Health.Value);
    }

    [Fact]
    public void MagicMirror_ShouldSwapAttackAndHealth()
    {
        var creature = new DummyCreature(3, 8);
        var spell = new MagicMirror();

        spell.Cast(creature);

        Assert.Equal(8, creature.Attack.Value);
        Assert.Equal(3, creature.Health.Value);
    }

    [Fact]
    public void ProtectionAmulet_ShouldBlockDamageAccordingToCharges()
    {
        ICreature creature = new DummyCreature(2, 10);
        creature = new ProtectionAmulet(2).Cast(creature);

        creature.TakeDamage(5);
        creature.TakeDamage(1);
        creature.TakeDamage(2);

        Assert.Equal(8, creature.Health.Value);
    }

    [Fact]
    public void ProtectionAmulet_ShouldStackWithExistingShield()
    {
        ICreature creature = new DummyCreature(1, 10);

        creature = new ProtectionAmulet(1).Cast(creature);
        creature = new ProtectionAmulet(2).Cast(creature);

        creature.TakeDamage(3);
        creature.TakeDamage(3);
        creature.TakeDamage(3);
        creature.TakeDamage(3);

        Assert.Equal(7, creature.Health.Value);
    }

    [Fact]
    public void UseSpellApply_ShouldWorkAsExtension()
    {
        var creature = new DummyCreature(4, 4);

        creature.Apply(new PowerPotion(3));

        Assert.Equal(7, creature.Attack.Value);
        Assert.Equal(4, creature.Health.Value);
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