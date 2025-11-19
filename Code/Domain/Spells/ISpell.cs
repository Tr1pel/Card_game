namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Spells;

public interface ISpell
{
    ICreature Cast(ICreature target);
}