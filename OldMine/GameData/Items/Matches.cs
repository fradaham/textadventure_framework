using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;

namespace OldMine.GameData.Items;

public class Matches : Item
{
    public override INoun Name => new EngNoun("matches");

    public override IEnumerable<INoun>? Synonyms => [new EngNoun("matchbox"), new EngNoun("box of matches")];

    public override string Description => "A damp matchbox, worn smooth at the corners. The matches inside are few, but still dry enough to spark in a pinch.";
}
