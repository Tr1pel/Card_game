namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Battle;

public interface IBattleEngine
{
    BattleOutcome Fight(IEnumerable<ICreature> player1Board, IEnumerable<ICreature> player2Board);
}