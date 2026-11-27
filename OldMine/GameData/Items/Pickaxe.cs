using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;

namespace OldMine.GameData.Items;

public class Pickaxe : Item
{
    public override INoun Name => new EngNoun("pickaxe");

    public override IEnumerable<INoun>? Synonyms => [new EngNoun("axe"), new EngNoun("miner pick")];

    public override string Description => "A rusted iron pickaxe with a cracked shaft and a worn head. It looks like it could break loose rock or from a desperate fight.";
}
