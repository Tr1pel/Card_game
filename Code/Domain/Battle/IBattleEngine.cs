using Itmo.ObjectOrientedProgramming.Lab3.Context.Board;

namespace Itmo.ObjectOrientedProgramming.Lab3.Battle;

public interface IBattleEngine
{
    BattleResult Fight(PlayerBoard player1Board, PlayerBoard player2Board);
}