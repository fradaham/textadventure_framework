using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;

namespace OldMine.GameData.Rooms;

public class StreamWay : Room
{
    public override INoun Name => new EngNoun("stream way");

    public override IEnumerable<INoun> Synonyms => [new EngNoun("stream"), new EngNoun("river"), new EngNoun("channel")];

    public override List<IEntity> Items { get; } = [];

    public override List<IExit> Exits { get; } =
    [
        new Exit
        {
            Name = new EngNoun("sunlit passage"),
            TargetRoomName = "forest trail",
            Description = "The water rises ahead into a narrow crack of daylight.",
            ExitMessage = "You climb along the stream as it climbs toward the surface, and the air turns warmer. A thin sliver of morning light appears ahead.",
            AllowedPrepositions = ["towards", "up", "along"]
        }
    ];

    public override string Description => "The stream is narrow but swift. It cuts through the stone like a scar and carries a breath of fresh air from somewhere above. The darkness is no longer complete; in the distance, a pale seam of daylight flashes.";

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        if (action.Predicate == Predicates.Go && action.PlaceAdverbial != null && action.PlaceAdverbial == Exits.First(e => e.TargetRoomName == "forest trail"))
        {
            return new ActionResult
            {
                Message = "You follow the stream upward, scrambling through the final crack in the rock. The air is sweet with wet pine and morning cold. You emerge from the earth into the forest again, breathing the open air like a drowning man learning the taste of the world.",
                GameResult = GameResult.Success
            };
        }

        return null;
    }
}
