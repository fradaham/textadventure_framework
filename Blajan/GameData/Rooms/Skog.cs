using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Blajan.GameData.Items;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Blajan.GameData.Rooms;

public class Skog : SwedishAbstractRoom
{
    public override INoun Name => new SweNoun("Skog", Genus.Utrum);

    public override List<IEntity> Items => [];

    public override List<IExit> Exits { get; } = [];

    public override IEnumerable<INoun> Synonyms => [];

    public override string Description => $"";
    
    public override ActionResult? Enter(Context context)
    {
        return new ActionResult()
        {
            Message = "När du stiger ut i skogen inser du att du är fri. Skogsdofterna av tallbarr och blåbärsris är friska och din näsa hämtar sig långsamt men säkert från det luftburna trauma som hemsökt blajans domän.",
            GameResult = GameResult.Success
        };
    }
}