namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.ValueObjects;

public record class AttackValue
{
    public AttackValue(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public AttackValue Add(int d) => new AttackValue(Value + d);
}