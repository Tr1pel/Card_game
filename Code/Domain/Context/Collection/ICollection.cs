using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Collection;

public interface ICollection
{
    IReadOnlyCollection<ICreature> OwnedPrototypes { get; }

    void AddToCollection(ICreature prototype);

    void RemoveFromCollection(ICreature prototype);
}