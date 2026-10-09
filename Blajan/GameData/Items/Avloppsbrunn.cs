using System.Runtime.CompilerServices;
using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Blajan.GameData.Items;

public class AvloppsBrunn : SwedishAbstractItem
{
    public override INoun Name => new SweNoun("avloppsbrunn", Genus.Utrum);

    public override IEnumerable<INoun> Synonyms =>
    [
        new SweNoun("brunn", Genus.Utrum),
    ];

    public override string Description => "Det är en rejäl brunn med ett raster i gjutjärn nedsänkt i betongen. Det hörs ett gurglande nedifrån brunnens djup. Du får en klart obehaglig känsla när du försöker titta ned i brunnens mörker - du känner dig iakttagen.";
    public override bool IsFixed => true;

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        if (action.Predicate == Predicates.Ta && action.DirectObject == this)
        {
            return new ActionResult()
            {
                Message = "Nog är du stark, men inte så stark. Den sitter väldigt mycket fast."
            };
        }
        return null;
    }
}