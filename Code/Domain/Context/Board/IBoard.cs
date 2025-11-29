using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Board;

public interface IBoard
{
    int MaxSlots { get; }

    IReadOnlyList<ICreature> Creatures { get; }

    void AddFromBoard(ICreature creature);

    void RemoveToBoard(ICreature creature);

    IEnumerable<ICreature> GetPotentialAttackers();

    IEnumerable<ICreature> GetPotentialTargets();
}