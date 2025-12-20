using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public sealed class CreatureDirector
{
    public ICreature Build(ICreatureBuilder builder, CreatureBuildPlan plan)
    {
        builder.WithAttack(plan.Attack);
        builder.WithHealth(plan.Health);

        foreach (IModifier modifier in plan.Modifiers)
        {
            builder.WithModifier(modifier);
        }

        return builder.Build();
    }
}