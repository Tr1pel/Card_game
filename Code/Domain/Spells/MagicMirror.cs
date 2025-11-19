namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Spells;

public sealed class MagicMirror : ISpell
{
    public ICreature Cast(ICreature target)
    {
        int a = target.Attack;
        int h = target.Health;

        target.ModifyAttack(h - a);
        target.ModifyHealth(a - h);

        return target;
    }
}