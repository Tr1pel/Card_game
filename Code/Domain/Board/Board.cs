namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Board;

public sealed class Board : IBoard
{
    private readonly List<ICreature> _creatures;

    public Board(int maxSlots = 7)
    {
        MaxSlots = maxSlots;
        _creatures = new List<ICreature>(maxSlots);
    }

    public int MaxSlots { get; }

    public IReadOnlyList<ICreature> Creatures => _creatures;

    public void AddFromBoard(ICreature creature)
    {
        if (_creatures.Count >= MaxSlots)
        {
            return;
        }

        _creatures.Add(creature);
    }

    public void RemoveToBoard(ICreature creature)
    {
        _creatures.Remove(creature);
    }

    public IEnumerable<ICreature> GetPotentialAttackers()
    {
        return _creatures.Where(c => c.IsAlive);
    }

    public IEnumerable<ICreature> GetPotentialTargets()
    {
        return _creatures.Where(c => c.IsAlive);
    }

    public void CleanDead()
    {
        _creatures.RemoveAll(c => !c.IsAlive);
    }
}