namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Collection;

public sealed class Collection
{
    private readonly List<ICreature> _owned;

    public Collection()
    {
        _owned = new List<ICreature>();
    }

    public IReadOnlyCollection<ICreature> OwnedPrototypes => _owned;

    public void AddOnwed(ICreature prototype)
    {
        _owned.Add(prototype);
    }

    public void RemoveOwmed(ICreature prototype)
    {
        _owned.Remove(prototype);
    }
}