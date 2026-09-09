using TextAdventure.Concepts;
using TextAdventure.Concepts.Implementations;
using TextAdventure.GameData.Items;
using TextAdventure.Language.Concepts;
using TextAdventure.Language.Swedish;

namespace TextAdventure.GameData.Rooms;

public class Tradgard : SwedishAbstractRoom
{
    public override INoun Name => new SweNoun("Trädgård", Genus.Utrum);

    public override List<IEntity> Items => 
    [
        new KallarDorr(),
    ];

    public override List<Exit> Exits { get; } = new();

    public override IEnumerable<INoun> Synonyms => [];

    public override string Description => $"Du står i en liten men innehållsrik trädgård, fylld ned rabatter och träd i en smakfull komposition. Den börjar se lite vildvuxen ut, men man kan se att den har varit väl omskött tidigare.";
    

    public override ActionResult? HandleAction(Context context, PlayerAction command)
    {
        return null;
    }
}