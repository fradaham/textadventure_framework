using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;

namespace OldMine.GameData.Items;

public class Timber : Item
{
    public override INoun Name => new EngNoun("timber");

    public override IEnumerable<INoun>? Synonyms => [new EngNoun("planks"), new EngNoun("boards"), new EngNoun("wood")];

    public override string Description => "A stack of old mine supports, cut to rough planks. The wood smells wet and resinous, but it is still sound enough to float a simple raft.";

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        if (action.Predicate == Predicates.Pick_up)
        {
            if (context.Player.Inventory.Any(i => i is Rope) && !context.Player.Inventory.Any(i => i is Raft))
            {
                context.Player.Inventory.Add(new Raft());
                return new ActionResult
                {
                    Message = "You tie the planks together with the rope and make a rough but steady raft. It creaks, but it will float."
                };
            }
        }

        return null;
    }
}
