using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public sealed class CreatureBuildPlan
{
    public CreatureBuildPlan(int attack, int health, IEnumerable<IModifier> modifiers)
    {
        Attack = attack;
        Health = health;
        Modifiers = modifiers.ToArray();
    }

    public static CreatureBuildPlan Empty { get; } = new CreatureBuildPlan(0, 0, Array.Empty<IModifier>());

    public int Attack { get; }

    public int Health { get; }

    public IReadOnlyCollection<IModifier> Modifiers { get; }
}