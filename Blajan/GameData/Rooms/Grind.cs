using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Blajan.GameData.Items;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Blajan.GameData.Rooms;

public class Grind : SwedishAbstractRoom
{
    public override INoun Name => new SweNoun("Grind", Genus.Utrum);

    public override List<IEntity> Items =>
    [
        new AvloppsBrunn()
    ];

    public override List<IExit> Exits { get; } =
    [
        new SwedishExit()
        {
            Name = new SweNoun("portal", Genus.Utrum),
            Synonyms = [
                new SweNoun("portöppning", Genus.Utrum),
                new SweNoun("port", Genus.Utrum)
            ],
            Description = "Det är en stenportal som är en del av den stenmur som kringgärdar trädgården.",
            ExitMessage = "Du öppnar grinden, den gnisslar till, och kliver in i porten ut från trädgården.",
            TargetRoomName = "Skog"
        }
    ];

    public override IEnumerable<INoun> Synonyms => [];

    public override string Description => $"Du står vid änden på trädgårdsgången invid en grind som sitter i en portal med ett valv som välver sig från vardera mursida. Det är en klassisk järngrind. Den är svartmålad och har franska liljor på spetsarna som sticker upp längs överkanten. \n\n Här i änden på gången är det en lågpunkt där en avloppsbrunn är placerad.";
    
    private bool hasBattledBlajan = false;
    public override ActionResult? HandleAction(Context context, PlayerAction command)
    {
        if (!hasBattledBlajan)
        {
            IExit portal = Exits.First(e => e.Name.Name == "portal");
            if (command.Predicate.Verb == Verbs.Gå && (command.DirectObject == portal || command.PlaceAdverbial == portal || command.MannerAdverbial == portal))
            {
                
                hasBattledBlajan = true;
                if (context.Player.Inventory.Any(i => i is Dynggrep))
                {
                    return new ActionResult()
                    {
                        Message = "Du blir attackerad av en blaja som likt en tjock tentakel ormar sig upp ur avloppsbrunnen! Du greppar ett stadigt tag om din trogna dynggrep och tackar tyst din lyckliga stjärna för att du plockade upp detta mäktiga vapen inför denna strid - en strid för dig liv!",
                        Fight = new Opponent()
                        {
                            FightingSkill = 10,
                            Health = 20,
                            Name = "Ärkeblajan"
                        }
                    };
                }
                else
                {
                    return new ActionResult()
                    {
                        Message = "Du blir attackerad av en blaja som likt en tjock tentakel ormar sig upp ur avloppsbrunnen! Skräckslaget inser du vilken mäktig fiende du står inför och måste slåss mot till döden!",
                        Fight = new Opponent()
                        {
                            FightingSkill = 20,
                            Health = 20,
                            Name = "Ärkeblajan"
                        }
                    };
                }
            }
        }
        return null;
    }
}