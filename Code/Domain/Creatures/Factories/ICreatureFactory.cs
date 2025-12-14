namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;

public interface ICreatureFactory
{
    string Id { get; }

    ICreature Create();
}