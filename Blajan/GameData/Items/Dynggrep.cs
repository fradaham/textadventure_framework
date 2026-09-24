using System.Runtime.CompilerServices;
using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Blajan.GameData.Items;

public class Dynggrep : SwedishAbstractItem
{
    public override INoun Name => new SweNoun("dynggrep", Genus.Utrum);

    public override IEnumerable<INoun> Synonyms =>
    [
        new SweNoun("grep", Genus.Utrum),
    ];

    public override string Description => "Det är en dynggrep av nättare modell, med fyra spetsar. Visst kanske man har skyfflat dynga med denna, men du gissar att grepen också har kommit till användning vid potatisupptaging här i trädgården.";

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        return null;
    }
}