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

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        if (action.Predicate == Predicates.Ät && action.DirectObject == this)
        {
            context.Player.Health -= 3;
            return new ActionResult()
            {
                Message = "Du smakar på ett hörn, vilket utan dröjsmål får din mage och strupe att transformeras till en kraftfull fontän. Effektivt tömmer du ut ditt maginnehåll mot den närmsta väggen. Hade det inte varit spya så hade du kunnat praktisera som en mänsklig högtryckstvätt. Alltså vafan, vad har hänt med farfars kvalitetsfalukorv!? Nu har någon skitit i den blå skåpet.",
            };
        }
        else if (action.Predicate == Predicates.Undersök && action.DirectObject == this)
        {
            return new ActionResult()
            {
                Message = "Hmmm....bäst före 830316."
            };
        }
        return null;
    }
}