namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.ValueObjects;

public record class HealthValue
{
    public HealthValue(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public HealthValue Add(int d) => new HealthValue(Value + d);

    public HealthValue Subtract(int d) => new HealthValue(Value - d);
}