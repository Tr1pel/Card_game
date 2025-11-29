namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Board;

public interface IBoard
{
    int MaxSlots { get; }

    IReadOnlyList<ICreature> Creatures { get; }

    void AddFromBoard(ICreature creature);

    void RemoveToBoard(ICreature creature);

    IEnumerable<ICreature> GetPotentialAttackers();

    IEnumerable<ICreature> GetPotentialTargets();
}