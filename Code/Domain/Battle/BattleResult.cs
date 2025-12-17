namespace Itmo.ObjectOrientedProgramming.Lab3.Battle;

public abstract record BattleResult
{
    private BattleResult() { }

    public sealed record Player1Win : BattleResult;

    public sealed record Player2Win : BattleResult;

    public sealed record Draw : BattleResult;
}