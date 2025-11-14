namespace Itmo.ObjectOrientedProgramming.Lab3.Domain;

public static class CreatureExtensions
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