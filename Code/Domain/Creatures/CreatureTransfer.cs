namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures;

public static class CreatureTransfer
{
    public static ICreature ToBattle(this ICreature boardCreature)
    {
        return boardCreature.CloneForNewContext();
    }

    public static ICreature ToBoard(this ICreature catalogCreature)
    {
        return catalogCreature.CloneForNewContext();
    }
}