using Itmo.ObjectOrientedProgramming.Lab3.Context.Board;

namespace Itmo.ObjectOrientedProgramming.Lab3.Battle;

public interface IBattleEngine
{
    ResultType Fight(IBoard player1Board, IBoard player2Board);
}