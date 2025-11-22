namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Catalog;

public interface ICatalog
{
    IReadOnlyCollection<ICreature> Prototypes { get; }

    void AddPrototype(ICreature prototype);

    void RemovePrototype(ICreature prototype);

    IEnumerable<ICreature> CreateForBoard(IEnumerable<ICreature> prototypes);

    ICreature CreateForBoard(ICreature prototype);
}