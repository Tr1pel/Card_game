using Itmo.ObjectOrientedProgramming.Lab3.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spells;

public sealed class MagicMirror : ISpell
{
    public ICreature Cast(ICreature target)
    {
        int a = target.Attack.Value;
        int h = target.Health.Value;

        target.ModifyAttack(h - a);
        target.ModifyHealth(a - h);

        return target;
    }
}