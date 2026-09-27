using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Blajan.GameData.Items;

public class Morakniv : SwedishAbstractItem
{
    public override INoun Name => new SweNoun("morakniv", Genus.Utrum);

    public override IEnumerable<INoun> Synonyms => [new SweNoun("kniv", Genus.Utrum)];

    public override string Description => $"En klassisk morakniv, med ljus läderslida och orange handtag i plast. Den måste vara producerad på 70-talet.";

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        if ((action.Predicate.Verb == Verbs.Släng || action.Predicate.Verb == Verbs.Sätt || action.Predicate.Verb == Verbs.Stick) && action.DirectObject == this && action.PlaceAdverbial is Falukorv)
        {
            return new ActionResult()
            {
                Message = "Falukorven blir helt vansinnig och slaskar till dig i fejan!",
                Fight = new Opponent()
                {
                    FightingSkill = 10,
                    Health = 15,
                    Name = "Farfars falukorv",
                    SuccessEvent = new ActionResult()
                    {
                        Message = "Du krämade farfars falukorv. Det gör ont i hjärtat...men kanske fanns det någon andemening med detta? Du inser att striden stärkte din stridsförmåga.",
                        Custom = (context) =>
                        {
                            if (context.Player.Room.Items.Contains(action.PlaceAdverbial))
                            {
                                context.Player.Room.Items.Remove(action.PlaceAdverbial);
                            }
                            else
                            {
                                context.Player.Inventory.Remove(action.PlaceAdverbial);
                            }
                            context.Player.FightingSkill += 5;
                        }
                    },
                    FailEvent = new ActionResult()
                    {
                        GameResult = GameResult.Fail,
                        Message = "Du blev så fett härskad av en härsken falukorv!"
                    }
                }                
            };
        }
        return null;
    }
}