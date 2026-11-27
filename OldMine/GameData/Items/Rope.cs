using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;

namespace OldMine.GameData.Items;

public class Rope : Item
{
    public override INoun Name => new EngNoun("rope");

    public override IEnumerable<INoun>? Synonyms => [new EngNoun("line"), new EngNoun("cord")];

    public override string Description => "A coil of thick hemp rope, knotted and worn smooth by damp hands. It would be useful for lashing timber together.";

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        if (action.Predicate == Nai.TextAdventure.Language.English.Predicates.Pick_up)
        {
            if (context.Player.Inventory.Any(i => i is Timber) && !context.Player.Inventory.Any(i => i is Raft))
            {
                context.Player.Inventory.Add(new Raft());
                return new ActionResult
                {
                    Message = "You lash the planks together with the rope and make a rough but steady raft. It creaks, but it will float."
                };
            }
        }

        return null;
    }
}
