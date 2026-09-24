using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Blajan.GameData.Items;

public class Falukorv : SwedishAbstractItem
{
    public override INoun Name => new SweNoun("falukorv", Genus.Utrum);

    public override IEnumerable<INoun> Synonyms => [new SweNoun("korv", Genus.Utrum)];

    public override string Description => $"En falukorv, och inte vilken sort som helst. Det är farfars märke. Du blir nostalgisk och tänker på farfar som var falukorvsfabrikant. Men varför ligger farfars falukorv här?";
}