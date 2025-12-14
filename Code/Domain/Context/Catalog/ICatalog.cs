using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public interface ICatalog
{
    IReadOnlyCollection<ICreature> Prototypes { get; }

    void AddToCatalog(ICreature prototype);

    void RemoveFromCatalog(ICreature prototype);

    ICreature CreateForBoard(ICreature prototype);
}