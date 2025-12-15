using Itmo.ObjectOrientedProgramming.Lab3.Battle;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Board;

public sealed class PlayerBoard : IBoard
{
    private readonly List<ICreature> _creatures;

    public PlayerBoard(int maxSlots = 7)
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

    public ICreature? GetAttacker(IRng rng)
    {
        var candidates = _creatures.Where(c => c.IsAlive && c.Attack.Value > 0).ToList();

        int indx = rng.NextInt(0, candidates.Count);
        return candidates[indx];
    }

    public ICreature? GetTarget(IRng rng)
    {
        var candidates = _creatures.Where(c => c.IsAlive).ToList();

        int indx = rng.NextInt(0, candidates.Count);
        return candidates[indx];
    }

    public void CleanDead()
    {
        _creatures.RemoveAll(c => !c.IsAlive);
    }
}