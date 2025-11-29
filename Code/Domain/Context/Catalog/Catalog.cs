using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public sealed class Catalog : ICatalog
{
    private readonly List<ICreature> _prototypes;

    public Catalog()
    {
        _prototypes = new List<ICreature>();
    }

    public IReadOnlyCollection<ICreature> Prototypes => _prototypes;

    public void AddToCatalog(ICreature prototype)
    {
        _prototypes.Add(prototype);
    }

    public void RemoveFromCatalog(ICreature prototype)
    {
        _prototypes.Remove(prototype);
    }

    public IEnumerable<ICreature> CreateForBoard(IEnumerable<ICreature> prototypes)
    {
        return prototypes.Select(p => p.ToBoard()).ToList();
    }

    public ICreature CreateForBoard(ICreature prototype)
    {
        return prototype.ToBoard();
    }
}