using System.Runtime.CompilerServices;
using TextAdventure.Concepts;
using TextAdventure.Concepts.Implementations;
using TextAdventure.Language.Concepts;
using TextAdventure.Language.Swedish;

namespace TextAdventure.GameData.Items;

public class Toapapper : SwedishAbstractItem
{
    public override INoun Name => new SweNoun("toalettpappersrulle", Genus.Utrum);

    public override IEnumerable<INoun> Synonyms =>
    [
        new SweNoun("dasspapper", Genus.Neutrum),
        new SweNoun("dasspappersrulle", Genus.Utrum),
        new SweNoun("toalettpapper", Genus.Neutrum)
    ];

    public override string Description => "Det är en helt vanlig rulle med dasspapper. Hälften av papperet är kvar på ett ungefär.";

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        return null;
    }
}