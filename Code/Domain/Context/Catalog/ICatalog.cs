using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public interface ICatalog
{
    IReadOnlyCollection<ICreatureFactory> Factories { get; }

    void AddFactory(ICreatureFactory factory);

    void RemoveFactory(ICreatureFactory factory);

    ICreature Create(string id);
}