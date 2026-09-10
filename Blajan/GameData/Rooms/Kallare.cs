using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Blajan.GameData.Items;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Blajan.GameData.Rooms;

public class Kallare : SwedishAbstractRoom
{
    public override INoun Name => new SweNoun("källare", Genus.Utrum);

    public override List<IEntity> Items {get; } =
    [
        new KallarDorr(),
        new Blaja(),
        new SwedishItem()
        {
            Name = new SweNoun("skräp", Genus.Neutrum),
            Synonyms = [new SweNoun("skräphög", Genus.Utrum)],
            Description = "Det är en skräphög i vilken det främst verkar ligga kartonger och annat pappersskräp. Men det är svårt att se vad som finns utan en närmare undersökning.",
            HandleAction = (context, action) => {
                if (action.Predicate.Verb == Verbs.Undersök)
                {
                    if (context.Player.Room.Items.Any(i => i is Morakniv) || context.Player.Inventory.Any(i => i is Morakniv))
                    {
                        return new ActionResult()
                        {
                            Message = "Du har redan letat igenom skräpet. Det är skräp. Rent skräp."
                        };
                    }
                    else
                    {
                        context.Player.Room.Items.Add(new Morakniv());
                        context.Player.Room.Items.Add(new Toapapper());
                        return new ActionResult()
                        {
                            Message = "I en kartong med papper och spillvirke finner du en morakniv. Du hittar också en rulle toalettpapper med hälften av papperet kvar. Det är det enda av värde som du hittar."
                        };
                    }
                }

                return null;
            }
        }
    ];

    public override List<IExit> Exits { get; } =
    [
        new SwedishExit()
        {
            Name = new SweNoun("dörröppning", Genus.Utrum),
            Synonyms =
            [
                new SweNoun("dörrhål", Genus.Utrum),
            ],
            Description = "Det verkar vara natt, men ett blekt månsken gör att du kan ana skuggor av träd därute, och du hör en svag vind som susar genom trädkronor.",
            TargetRoom = "Trädgård",
            ExitMessage = "Du kliver ut genom dörröppningen och föser några långa grässtrån åt sidan som växer på utsidan av dörrkarmens tröskel. Frisk utomhusluft ersätter den unkna doft som du nödgats inandas i källaren.",
            IsActivated = false
        }
    ];

    public override IEnumerable<INoun> Synonyms => [];


    public override string Description => $"Du är i en fuktig källare utan fönster. Det finns en gammal tjärad trädörr. Det gurglar någonstans och en blaja sväller på golvet. I ett hörn ligger en hög med skräp.";
    

    public override ActionResult? HandleAction(Context context, PlayerAction command)
    {
        return null;
    }
}