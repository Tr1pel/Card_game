using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Collection;

public sealed class Collection : ICollection
{
    private readonly List<ICreature> _owned;

    public Collection()
    {
        _owned = new List<ICreature>();
    }

    public IReadOnlyCollection<ICreature> OwnedPrototypes => _owned;

    public void AddToCollection(ICreature prototype)
    {
        _owned.Add(prototype);
    }

    public void RemoveFromCollection(ICreature prototype)
    {
        _owned.Remove(prototype);
    }
}