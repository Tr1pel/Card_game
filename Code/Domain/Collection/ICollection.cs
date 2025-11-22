namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Collection;

public interface ICollection
{
    IReadOnlyCollection<ICreature> OwnedPrototypes { get; }

    void AddOwmed(ICreature prototype);

    void RemoveOwmed(ICreature prototype);
}