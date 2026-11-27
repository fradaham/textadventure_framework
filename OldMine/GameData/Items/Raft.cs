using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;

namespace OldMine.GameData.Items;

public class Raft : Item
{
    public override INoun Name => new EngNoun("raft");

    public override IEnumerable<INoun>? Synonyms => [new EngNoun("float"), new EngNoun("boat")];

    public override string Description => "A simple raft of lashed planks and rope. It looks flimsy, but it can carry a single person over a narrow lake or stream.";
}
