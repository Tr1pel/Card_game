namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Catalog;

public interface ICatalog
{
    IReadOnlyCollection<ICreature> Prototypes { get; }

    void AddToCatalog(ICreature prototype);

    void RemoveFromCatalog(ICreature prototype);

    IEnumerable<ICreature> CreateForBoard(IEnumerable<ICreature> prototypes);

    ICreature CreateForBoard(ICreature prototype);
}