using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;
using OldMine.GameData.Items;

namespace OldMine.GameData.Rooms;

public class ToolRoom : Room
{
    public override INoun Name => new EngNoun("tool room");

    public override IEnumerable<INoun> Synonyms => [new EngNoun("storage chamber"), new EngNoun("forge room")];

    public override List<IEntity> Items { get; } = [new Pickaxe()];

    public override List<IExit> Exits { get; } =
    [
        new Exit
        {
            Name = new EngNoun("lower gallery"),
            TargetRoomName = "lower gallery",
            Description = "The tunnel leading into the mine from the entrance.",
            ExitMessage = "You return to the gallery.",
            AllowedPrepositions = ["into", "to"]
        }
    ];

    public override string Description => "The old miner's storage room is empty apart from rusted hooks and a thick layer of dust. The smell of iron and cedar hangs in the air like a memory, but the way back to the gallery is gone behind you and there is no comforting light here.";
}
