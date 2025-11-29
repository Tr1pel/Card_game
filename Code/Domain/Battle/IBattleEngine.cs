using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Battle;

public interface IBattleEngine
{
    BattleOutcome Fight(IEnumerable<ICreature> player1Board, IEnumerable<ICreature> player2Board);
}