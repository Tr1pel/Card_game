namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Collection;

public interface ICollection
{
    IReadOnlyCollection<ICreature> OwnedPrototypes { get; }

    void AddToCollection(ICreature prototype);

    void RemoveFromCollection(ICreature prototype);
}