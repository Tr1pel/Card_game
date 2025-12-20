using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public sealed class PlayerCatalog : ICatalog
{
    private readonly List<ICreatureFactory> _factories;
    private readonly List<CreatureDirector> _directors;

    public PlayerCatalog()
    {
        _factories = new List<ICreatureFactory>();
        _directors = new List<CreatureDirector>();
    }

    public IReadOnlyCollection<ICreatureFactory> Factories => _factories;

    public IReadOnlyCollection<CreatureDirector> Directors => _directors;

    public void AddFactory(ICreatureFactory factory)
    {
        _factories.Add(factory);
    }

    public void RemoveFactory(ICreatureFactory factory)
    {
        _factories.Remove(factory);
    }

    public void AddDirector(CreatureDirector director)
    {
        _directors.Add(director);
    }

    public ICreatureBuilder Configure(string id)
    {
        ICreatureFactory? factory = _factories.FirstOrDefault(f => string.Equals(f.Id, id, StringComparison.Ordinal));
        if (factory is null)
        {
            throw new InvalidOperationException($"Фабрика {id} не найдена в каталоге");
        }

        return new CreatureBuilder(factory);
    }

    public ICreature Create(string id)
    {
        return Create(id, CreatureBuildPlan.Empty);
    }

    public ICreature Create(string id, CreatureBuildPlan plan)
    {
        ICreatureBuilder builder = Configure(id);
        CreatureDirector? director = _directors.FirstOrDefault();
        if (director is null)
        {
            throw new InvalidOperationException("В каталоге нет доступного директора");
        }

        return director.Build(builder, plan);
    }
}