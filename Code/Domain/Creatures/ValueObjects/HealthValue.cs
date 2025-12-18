namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.ValueObjects;

public record class HealthValue
{
    public HealthValue(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public HealthValue Add(int d)
    {
        int newValue = Value + d;
        if (newValue < 0)
        {
            newValue = 0;
        }

        return new HealthValue(newValue);
    }

    public HealthValue Subtract(int d)
    {
        int newValue = Value - d;
        if (newValue < 0)
        {
            newValue = 0;
        }

        return new HealthValue(newValue);
    }
}