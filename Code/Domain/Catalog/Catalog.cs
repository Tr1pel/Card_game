namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Catalog;

public sealed class Catalog : ICatalog
{
    private readonly List<ICreature> _prototupes;

    public Catalog()
    {
        _prototupes = new List<ICreature>();
    }

    public IReadOnlyCollection<ICreature> Prototypes => _prototupes;

    public void AddPrototype(ICreature prototype)
    {
        _prototupes.Add(prototype);
    }

    public void RemovePrototype(ICreature prototype)
    {
        _prototupes.Remove(prototype);
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