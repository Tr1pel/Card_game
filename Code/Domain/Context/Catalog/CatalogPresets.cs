using Itmo.ObjectOrientedProgramming.Lab3.Creatures.Factories;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public static class CatalogPresets
{
    public static ICatalog AddPrototypes(this ICatalog catalog)
    {
        catalog.AddFactory(new AmuletMasterFactory());
        catalog.AddFactory(new BattleAnalystFactory());
        catalog.AddFactory(new MimicChestFactory());
        catalog.AddFactory(new EvilFighterFactory());
        catalog.AddFactory(new ImmortalHorrorFactory());

        return catalog;
    }
}