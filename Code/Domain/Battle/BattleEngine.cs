using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Battle;

public sealed class BattleEngine : IBattleEngine
{
    private readonly IRng _rng;

    public BattleEngine(IRng rng)
    {
        _rng = rng;
    }

    public BattleOutcome Fight(IEnumerable<ICreature> player1Board, IEnumerable<ICreature> player2Board)
    {
        var p1 = player1Board.Select(c => c.ToBattle()).ToList();
        var p2 = player2Board.Select(c => c.ToBattle()).ToList();

        bool player1Turn = true;

        while (true)
        {
            List<ICreature> current = player1Turn ? p1 : p2;
            List<ICreature> opponent = player1Turn ? p2 : p1;

            ICreature? attacker = PickAttacker(current);
            ICreature? target = PickTarget(opponent);

            if (attacker is null)
            {
                if (target is null)
                {
                    return BattleOutcome.Draw;
                }

                if (target.Attack <= 0)
                {
                    return BattleOutcome.Draw;
                }

                player1Turn = !player1Turn;
                continue;
            }

            if (target is null)
            {
                return player1Turn ? BattleOutcome.Player1Win : BattleOutcome.Player2Win;
            }

            attacker.AttackTarget(target);
            player1Turn = !player1Turn;
        }
    }

    private ICreature PickRandom(List<ICreature> list)
    {
        int indx = _rng.NextInt(0, list.Count);
        return list[indx];
    }

    private ICreature? PickAttacker(IReadOnlyList<ICreature> side)
    {
        var candidates = side.Where(c => c.IsAlive && c.Attack > 0).ToList();
        if (candidates.Count == 0)
        {
            return null;
        }

        return PickRandom(candidates);
    }

    private ICreature? PickTarget(IReadOnlyList<ICreature> side)
    {
        var candidates = side.Where(c => c.IsAlive).ToList();
        if (candidates.Count == 0)
        {
            return null;
        }

        return PickRandom(candidates);
    }
}