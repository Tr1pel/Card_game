namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;

public sealed class DummyCreature : Creature
{
    public DummyCreature(int attack, int health) : base(attack, health) { }

    protected override Creature Instantiate(int attack, int health)
    {
        return new DummyCreature(attack, health);
    }
}