namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.ValueObjects;

public record class AttackValue
{
    public AttackValue(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public AttackValue Add(int d)
    {
        int newValue = Value + d;
        if (newValue < 0)
        {
            newValue = 0;
        }

        return new AttackValue(newValue);
    }
}