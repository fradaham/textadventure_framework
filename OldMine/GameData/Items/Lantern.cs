using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;

namespace OldMine.GameData.Items;

public class Lantern : Item
{
    public bool IsLit { get; private set; }

    public override INoun Name => new EngNoun("lantern");

    public override IEnumerable<INoun>? Synonyms => [new EngNoun("lamp"), new EngNoun("torch")];

    public override string Description => IsLit
        ? "The lantern burns with a low, steady yellow glow, pushing back the dark in a rough circle around your feet."
        : "A battered brass lantern, still heavy with old oil. The glass panes are smeared with soot, but the wick is dry enough to burn if you light it.";

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        if (action.Predicate == Predicates.Light && action.DirectObject == this)
        {
            if (IsLit)
            {
                return new ActionResult { Message = "The lantern is already lit." };
            }

            bool hasMatches = context.Player.Inventory.Any(i => i is Matches) || (action.MannerAdverbial is Matches);
            if (!hasMatches)
            {
                return new ActionResult { Message = "You need a source of flame to light the lantern." };
            }

            IsLit = true;
            return new ActionResult { Message = "You strike the match and coax a warm yellow glow from the lantern. The darkness retreats just enough to make the tunnel walls visible." };
        }

        return null;
    }
}
