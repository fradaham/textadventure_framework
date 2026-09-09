using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Blajan.GameData.Items;

public class Morakniv : SwedishAbstractItem
{
    public override INoun Name => new SweNoun("morakniv", Genus.Utrum);

    public override IEnumerable<INoun> Synonyms => [new SweNoun("kniv", Genus.Utrum)];

    public override string Description => $"En klassisk morakniv, med ljus läderslida och orange handtag i plast. Den måste vara producerad på 70-talet.";

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        return null;
    }
}