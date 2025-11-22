namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Board;

public interface IPlayerBoard
{
    int MaxSlots { get; }

    IReadOnlyList<ICreature> Creatures { get; }

    bool Remove(ICreature creature);

    bool Add(ICreature creature);

    IEnumerable<ICreature> GetPotentialAttackers();

    IEnumerable<ICreature> GetPotentialTargets();
}