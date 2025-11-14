using CardGame.Sample;
using Xunit;

namespace DefaultNamespace;

public class Test
{
    [Fact]
    public void CreateAll_Returns_List_With_Accessible_Properties()
    {
        var all = CreatureSamples.CreateAll();

        Assert.NotNull(all);
        Assert.NotEmpty(all);

        Assert.All(all, c =>
        {
            Assert.NotNull(c);
            Assert.NotNull(c.Stats);

            // Доступ к свойствам (без дополнительной логики/валидаций)
            _ = c.Name;
            _ = c.Stats.Health;
            _ = c.Stats.Attack;
        });
    }
}