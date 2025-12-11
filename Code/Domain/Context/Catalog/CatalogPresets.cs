using Itmo.ObjectOrientedProgramming.Lab3.Creatures.ObjectsCreatures;
using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Catalog;

public static class CatalogPresets
{
    public static ICatalog AddPrototypes(this ICatalog catalog)
    {
        catalog.AddToCatalog(new AmuletMaster().WithMagicShield().WithAttackMastery());

        catalog.AddToCatalog(new BattleAnalyst());
        catalog.AddToCatalog(new MimicChest());
        catalog.AddToCatalog(new EvilFighter());
        catalog.AddToCatalog(new ImmortalHorror());

        return catalog;
    }
}