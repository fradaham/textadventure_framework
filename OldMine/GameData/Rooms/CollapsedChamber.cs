using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;
using OldMine.GameData.Items;

namespace OldMine.GameData.Rooms;

public class CollapsedChamber : Room
{
    public override INoun Name => new EngNoun("collapsed chamber");

    public override IEnumerable<INoun> Synonyms => [new EngNoun("collapse"), new EngNoun("chamber")];

    public override List<IEntity> Items { get; } = [new Rope(), new Timber()];

    public override List<IExit> Exits { get; } =
    [
        new Exit
        {
            Name = new EngNoun("black water chamber"),
            TargetRoomName = "black water chamber",
            Description = "A narrow fissure opens downward toward a black lake beyond the broken stone.",
            ExitMessage = "You slip through the crack and descend toward the echoing lake.",
            AllowedPrepositions = ["towards", "into"]
        }
    ];

    public override string Description => "The collapse has turned the tunnel into a narrow pocket of broken rock. One of the mine supports split loose under the weight, and a rough stack of timber lies in the rubble. The silence feels alive, as though the mountain is waiting to hear your next breath.";
}
