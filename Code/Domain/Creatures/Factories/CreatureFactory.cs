namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;

public abstract class CreatureFactory : ICreatureFactory
{
    public string Id { get; }

    protected CreatureFactory(string id)
    {
        Id = id;
    }

    public ICreature Create()
    {
        return CreateInternal();
    }

    protected abstract ICreature CreateInternal();
}