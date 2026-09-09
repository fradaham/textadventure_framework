using System.Runtime.CompilerServices;
using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Blajan.GameData.Items;

public class Blaja : SwedishAbstractItem
{
    public override INoun Name => new SweNoun("blaja", Genus.Utrum);

    public override IEnumerable<INoun> Synonyms => [];

    public override string Description => "Det är en glänsande blaja, som på något mystiskt sätt verkar svälla på ett pulserande vis. Då och då spricker stigande bubblor upp ur blajan med ett smackande ljud. Hur konstigt det än kan låta så avger den ett svagt gul-grönt ljus. Det gör att du ser något i den annars bäcksvarta källaren. Men denna fördel kompenserar inte för den skarpt övermogna odören som fyller rummet. Du får en känsla av en inre ondska som strålar ut från blajan. Här kan du inte stanna.";

    private bool hasProducedKey = false;

    private bool persistentStupido = false;

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        if (action.Predicate.Verb == Verbs.Undersök)
        {
            if (action.MannerAdverbial is Morakniv)
            {
                if (hasProducedKey == false)
                {
                    context.Player.Room.Items.Add(new KallarNyckel());
                    hasProducedKey = true;
                    return new ActionResult()
                    {
                        Message = "Du böjer dig ner och med en inre motvilja sticker du ner morakniven i blajan. Du försöker att hålla fingrarna så långt bort som möjligt från den vidriga ytan med bubblorna. Du kan svära på att blajan utstrålar en pyrande illvilja när du sticker kniven i den. Du känner det underliggande golvet med knivspetsen, men plötsligt går spetsen emot något föremål som ligger där nere i blajan. Försiktigt fiskar du upp föremålet med kniven. Det är en gammal nyckel av större sort. När du reser dig upp ramlar nyckeln ner på golvet en bit från blajan med ett ljudligt plask."
                    };
                }
                else
                {
                    return new ActionResult()
                    {
                        Message = "Du sticker ner kniven i blajan igen, men det enda du känner är golvet under blajan, och en vidrig stank som fräter i dina näsborrar."
                    };
                }
            }
            else if (action.MannerAdverbial != null)
            {
                return new ActionResult()
                {
                    Message = $"Du vill inte klämma ner {action.MannerAdverbial.Name.DefiniteForm} i blajan."
                };
            }
            else
            {
                if (persistentStupido)
                {
                    return new ActionResult()
                    {
                        Message = $"Du tvingar envist ner dina fingar i blajan för att känna på den, och en stickande känsla rör sig uppför armen. Du kan plötsligt inte röra dig! Skräckslaget ser du hur blajan börjar att vandra uppför din arm varefter köttet på din arm fräts bort. En våg går genom blajan som nu böjer sig över dig och omsluter dig helt medans du skriker i dödsplågor. Ditt liv ändar här, inuti en lömsk blaja.",
                        StoryEvent = StoryEvent.Fail
                    };
                }
                else
                {
                    persistentStupido = true;
                    return new ActionResult()
                    {
                        Message = $"Vadå, skulle du vilja sticka ner fingrarna i den där? Glöm! Försök inte det där igen!"
                    };
                }
            }
        }
        else if(action.Predicate.Verb == Verbs.Ta)
        {
            if (persistentStupido)
            {
                return new ActionResult()
                {
                    Message = $"Du tvingar envist ner dina fingar i blajan och försöker greppa om så mycket du kan. En stickande känsla rör sig uppför armen. Du kan plötsligt inte röra dig! Skräckslaget ser du hur blajan börjar att vandra uppför din arm varefter köttet på din arm fräts bort. En våg går genom blajan som nu böjer sig över dig och omsluter dig helt medans du skriker i dödsplågor. Ditt liv ändar här, inuti en lömsk blaja.",
                    StoryEvent = StoryEvent.Fail
                };
            }
            else
            {
                persistentStupido = true;
                return new ActionResult()
                {
                    Message = $"Säkert. Du kan ju ta och suga upp den med din mun om du vill. Det verkar ungefär lika mumsigt att försöka ta den där vidriga och bubblande slemblobben med händerna."
                };
            }
        }

        return null;
    }
}