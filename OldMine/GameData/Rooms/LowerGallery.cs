using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;
using OldMine.GameData.Items;

namespace OldMine.GameData.Rooms;

public class LowerGallery : Room
{
    private bool _collapseTriggered;

    public override INoun Name => new EngNoun("lower gallery");

    public override IEnumerable<INoun> Synonyms => [new EngNoun("gallery"), new EngNoun("tunnel")];

    public override List<IEntity> Items { get; } = [];

    public override List<IExit> Exits { get; } =
    [
        new Exit
        {
            Name = new EngNoun("mine entrance"),
            TargetRoomName = "mine entrance",
            Description = "The old mouth of the mine is a thin rectangle of night behind you.",
            ExitMessage = "The path back rises toward the surface.",
            AllowedPrepositions = ["back", "towards", "up"]
        },
        new Exit
        {
            Name = new EngNoun("collapsed chamber"),
            TargetRoomName = "collapsed chamber",
            Description = "A broken drift leads deeper under the hill, where the stone is darker and the air is colder.",
            ExitMessage = "You push through the old drift and head deeper.",
            AllowedPrepositions = ["into", "through", "towards"]
        },
        new Exit
        {
            Name = new EngNoun("tool room"),
            TargetRoomName = "tool room",
            Description = "A side passage leads toward an abandoned tool chamber where miners once stored their gear.",
            ExitMessage = "You step into a narrow side chamber and pause in the old miner's stores.",
            AllowedPrepositions = ["into", "towards"]
        }
    ];

    public override string Description => "The gallery narrows and the ceiling drops low. Iron spikes and rusted timber are all that remain of old supports. Every echo sounds wrong, as if the stone is listening back.";

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        if (!_collapseTriggered && action.Predicate == Predicates.Go && action.PlaceAdverbial != null)
        {
            var collapseExit = Exits.FirstOrDefault(e => e.TargetRoomName == "collapsed chamber");
            var returnExit = Exits.FirstOrDefault(e => e.TargetRoomName == "mine entrance");
            var toolExit = Exits.FirstOrDefault(e => e.TargetRoomName == "tool room");
            if (collapseExit != null && action.PlaceAdverbial == collapseExit)
            {
                _collapseTriggered = true;
                if (returnExit != null)
                {
                    returnExit.IsActivated = false;
                }
                if (toolExit != null)
                {
                    toolExit.IsActivated = false;
                }

                return new ActionResult
                {
                    Message = "The ceiling groans. A deafening crack erupts overhead and a blast of stone tears down across the tunnel behind you. The way back to the entrance collapses into a solid wall of rubble, and the side chamber you were about to explore is cut off. You are trapped in the mine.",
                    MoveToRoomId = "collapsed chamber"
                };
            }
        }

        return null;
    }
}
