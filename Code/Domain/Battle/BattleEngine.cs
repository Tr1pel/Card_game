using Itmo.ObjectOrientedProgramming.Lab3.Context.Board;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Battle;

public sealed class BattleEngine : IBattleEngine
{
    private readonly IRng _rng;

    public BattleEngine(IRng rng)
    {
        _rng = rng;
    }

    public BattleResult Fight(PlayerBoard player1Board, PlayerBoard player2Board)
    {
        PlayerBoard p1 = CloneToBoard(player1Board);
        PlayerBoard p2 = CloneToBoard(player2Board);

        bool player1Turn = true;

        while (true)
        {
            PlayerBoard current = player1Turn ? p1 : p2;
            PlayerBoard opponent = player1Turn ? p2 : p1;

            ICreature? attacker = current.GetAttacker(_rng);
            ICreature? target = opponent.GetTarget(_rng);

            if (attacker is null)
            {
                if (target is null)
                {
                    return new BattleResult.Draw();
                }

                if (target.Attack.Value == 0)
                {
                    return new BattleResult.Draw();
                }

                player1Turn = !player1Turn;
                continue;
            }

            if (target is null)
            {
                return player1Turn ? new BattleResult.Player1Win() : new BattleResult.Player2Win();
            }

            attacker.AttackTarget(target);
            player1Turn = !player1Turn;
        }
    }

    private static PlayerBoard CloneToBoard(PlayerBoard source)
    {
        var cloned = new PlayerBoard(source.MaxSlots);
        foreach (ICreature creature in source.Creatures)
        {
            cloned.AddFromBoard(creature.ToBattle());
        }

        return cloned;
    }
}