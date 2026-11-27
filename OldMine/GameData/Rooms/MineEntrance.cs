using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;
using OldMine.GameData.Items;

namespace OldMine.GameData.Rooms;

public class MineEntrance : Room
{
    public override INoun Name => new EngNoun("mine entrance");

    public override IEnumerable<INoun> Synonyms => [new EngNoun("entrance"), new EngNoun("mine mouth")];

    public override List<IEntity> Items { get; } = [new Matches()];

    public override List<IExit> Exits { get; } =
    [
        new Exit
        {
            Name = new EngNoun("forest trail"),
            TargetRoomName = "forest trail",
            Description = "The faint scent of pine and wet earth drifts back from the trail outside.",
            ExitMessage = "You step back out into the forest and the roar of the mine fades behind you.",
            AllowedPrepositions = ["out", "back", "towards"]
        },
        new Exit
        {
            Name = new EngNoun("lower gallery"),
            TargetRoomName = "lower gallery",
            Description = "A narrow descending tunnel leads into the darkness beneath the hill.",
            ExitMessage = "You descend into the old gallery, where the air turns cold and the silence thickens.",
            AllowedPrepositions = ["into", "down", "towards"]
        }
    ];

    public override string Description => "The mine mouth is half-collapsed, ringed by broken beams and slick stone. Rain drips through the ruined timbering and gathers in a black pool at the threshold. A draft breathes from inside, carrying the smell of damp iron, wet clay, and something old and root-like. The darkness beyond the entrance is so complete that you would never venture deeper without a flame.";

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        if (action.Predicate == Predicates.Go && action.PlaceAdverbial != null && action.PlaceAdverbial == Exits.First(e => e.TargetRoomName == "lower gallery"))
        {
            Lantern? lantern = context.Player.Inventory.OfType<Lantern>().FirstOrDefault();
            if (lantern == null || !lantern.IsLit)
            {
                return new ActionResult
                {
                    Message = "You don't dare step into the lower gallery without light. The dark is too deep and the air too still."
                };
            }
        }

        return null;
    }
}
