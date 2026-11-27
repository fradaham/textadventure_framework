using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;
using OldMine.GameData.Items;

namespace OldMine.GameData.Rooms;

public class ForestTrail : Room
{
    public override INoun Name => new EngNoun("forest trail");

    public override IEnumerable<INoun> Synonyms => [new EngNoun("forest"), new EngNoun("trail"), new EngNoun("woods")];

    public override List<IEntity> Items { get; } = [new Lantern()];

    public override List<IExit> Exits { get; } =
    [
        new Exit
        {
            Name = new EngNoun("mine entrance"),
            Synonyms = [new EngNoun("entrance"), new EngNoun("old mine")],
            TargetRoomName = "mine entrance",
            Description = "A narrow opening in the hillside, slumped with broken timber and old iron rails.",
            ExitMessage = "You force your way through the thorns and step into the shadowy mouth of the old mine.",
            AllowedPrepositions = ["into", "towards", "to", "through"]
        }
    ];

    public override string Description => "Rain lashes the forest in thin sheets. The air is cold and heavy with the smell of wet bark and moss. The trees lean inward as if listening. Somewhere behind the fir trunks, a small opening in the hillside glimmers like a wound in the earth.";

    public override ActionResult? Enter(Context context)
    {
        return new ActionResult
        {
            Message = "Rain drums on the leaves as you wander lost beneath the pines. The path is slick with mud, and a rusted sign points toward an old mine cut into the hillside, half-swallowed by roots and dripping stone. It looks like shelter, and maybe salvation."
        };
    }
}
