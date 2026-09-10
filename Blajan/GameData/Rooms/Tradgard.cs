using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Blajan.GameData.Items;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Blajan.GameData.Rooms;

public class Tradgard : SwedishAbstractRoom
{
    public override INoun Name => new SweNoun("Trädgård", Genus.Utrum);

    public override List<IEntity> Items => 
    [];

    public override List<IExit> Exits { get; } =
    [
        new SwedishExit()
        {
            Name = new SweNoun("Källare", Genus.Utrum),
            Synonyms =
            [],
            Description = "Källaren är den enda som är kvar av ett hus som har funnits här i sluttningen tidigare. Den sida som vätter mot trädgården är ovan jord, och i källarväggen av betong finns en öppen dörr.",
            TargetRoom = "Källare",
            ExitMessage = "Du kliver in genom dörröppningen till källaren. Unken luft slår emot dig som en vägg.",
        }
    ];

    public override IEnumerable<INoun> Synonyms => [];

    public override string Description => $"Du står i en liten men innehållsrik trädgård, fylld ned rabatter och träd i en smakfull komposition. Den börjar se lite vildvuxen ut, men man kan se att den har varit väl omskött tidigare.";
    

    public override ActionResult? HandleAction(Context context, PlayerAction command)
    {
        return null;
    }
}