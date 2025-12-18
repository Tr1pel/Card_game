using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public interface ICatalog
{
    IReadOnlyCollection<ICreatureFactory> Factories { get; }

    void AddFactory(ICreatureFactory factory);

    void RemoveFactory(ICreatureFactory factory);

    ICreatureBuilder Configure(string id);

    ICreature Create(string id);
}