namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Creatures;

public sealed class AmuletMaster : Creature
{
    public AmuletMaster() : base(attack: 5, health: 2) { }

    private AmuletMaster(int attack, int health) : base(attack, health) { }

    protected override Creature Instantiate(int attack, int health)
    {
        return new AmuletMaster(attack, health);
    }
}

// не забыть наложить модификаторы