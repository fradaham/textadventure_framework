using TextAdventure.Concepts;
using TextAdventure.Concepts.Implementations;
using TextAdventure.Language.Concepts;
using TextAdventure.Language.Swedish;

namespace TextAdventure.GameData.Items;

public class KallarNyckel : SwedishAbstractItem
{
    public override INoun Name => new SweNoun("järnnyckel", Genus.Utrum);

    public override IEnumerable<INoun> Synonyms => 
    [
        new SweNoun("nyckel", Genus.Utrum)
    ];

    private bool isSuperYucky = true;

    public override string Description => $"Det är en grov nyckel gjord i järn, av gammaldags sort. Den är {(isSuperYucky? "väldigt" : "något")} kladdig med äckligt blajslem.";
    

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        
        if (action.Predicate.Verb == Verbs.Ta && action.DirectObject == this)
        {
            if (!context.Player.Inventory.Contains(this))
            {
                if (isSuperYucky)
                {
                    return new ActionResult()
                    {
                        Message = $"Jag tar inte i den där jätteäckliga nyckeln. Det är mer blaja än nyckel."
                    };
                }
                else
                {
                    context.Player.Inventory.Add(this);
                    context.Player.Room.Items.Remove(this);
                    ActionResult result =  new ActionResult()
                    {
                        Message = $"Med viss motvilja tog du upp den slemmiga järnnyckeln med dina fingrar. "
                    };
                    return result;
                }
            }
            else
            {
                return new ActionResult()
                {
                    Message = $"Du har redan tagit upp nyckeln."
                };
            }
        }
        else if (action.Predicate.Verb == Verbs.Torka || action.Predicate.Verb == Verbs.Torka_av)
        {
            if (action.DirectObject == this)
            {
                if (isSuperYucky == true)
                {
                    if (action.MannerAdverbial is Toapapper)
                    {
                        isSuperYucky = false;
                        return new ActionResult()
                        {
                            Message = "Du avlägsnar så mycket slemmig blaja som du kan med hjälp av toalettpapperet. Det är omöjligt att få bort allt, men nu droppar det i alla fall inte om nyckeln."
                        };
                    }
                    else 
                    {
                        return new ActionResult()
                        {
                            Message = "Det fungerar inte."
                        };
                    }
                }
                else
                {
                    return new ActionResult()
                    {
                        Message = "Du har redan torkat av så mycket som det går."
                    };
                }
            }
        }
        
        return null;
        
    }
}