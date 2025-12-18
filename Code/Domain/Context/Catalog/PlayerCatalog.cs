using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public sealed class PlayerCatalog : ICatalog
{
    private readonly List<ICreatureFactory> _factories;

    public PlayerCatalog()
    {
        _factories = new List<ICreatureFactory>();
    }

    public IReadOnlyCollection<ICreatureFactory> Factories => _factories;

    public void AddFactory(ICreatureFactory factory)
    {
        _factories.Add(factory);
    }

    public void RemoveFactory(ICreatureFactory factory)
    {
        _factories.Remove(factory);
    }

    public ICreatureBuilder Configure(string id)
    {
        ICreatureFactory? factory = _factories.FirstOrDefault(f => string.Equals(f.Id, id, StringComparison.Ordinal));
        if (factory is null)
        {
            throw new InvalidOperationException($"Фабрика {id} не найдена в каталоге");
        }

        return new CreatureBuilder(factory.Create);
    }

    public ICreature Create(string id)
    {
        return Configure(id).Build();
    }
}