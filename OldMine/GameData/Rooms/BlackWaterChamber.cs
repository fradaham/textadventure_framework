using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Concepts.Implementations;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;

namespace OldMine.GameData.Rooms;

public class BlackWaterChamber : Room
{
    private bool _wraithAlive = true;

    public override INoun Name => new EngNoun("black water chamber");

    public override IEnumerable<INoun> Synonyms => [new EngNoun("lake chamber"), new EngNoun("water cave")];

    public override List<IEntity> Items { get; } = [];

    public override List<IExit> Exits { get; } =
    [
        new Exit
        {
            Name = new EngNoun("stream way"),
            TargetRoomName = "stream way",
            Description = "A dark stream heads away from the pool and vanishes beneath the stones.",
            ExitMessage = "You push away from the black water and let the current carry you toward the hidden stream.",
            AllowedPrepositions = ["towards", "through", "into"]
        },
        new Exit
        {
            Name = new EngNoun("collapsed chamber"),
            TargetRoomName = "collapsed chamber",
            Description = "A rubble of stone and timber that was once a mine chamber",
            ExitMessage = "You climb back into the collapsed chamber",
            AllowedPrepositions = ["into", "to", "towards"]
        }
    ];

    public override string Description => "The chamber opens into a lake of black water held in a crack in the rock. The surface reflects no light, only the pale, trembling glow of your lantern. The water is still, but every ripple seems to come from something below the surface.";

    public override ActionResult? HandleAction(Context context, PlayerAction action)
    {
        if (action.Predicate == Predicates.Go && action.PlaceAdverbial != null && action.PlaceAdverbial == Exits.First(e => e.TargetRoomName == "stream way"))
        {
            if (!context.Player.Inventory.Any(i => i is OldMine.GameData.Items.Raft))
            {
                return new ActionResult
                {
                    Message = "Even if you have the basic knowledge of swimming, you don't dare letting your body sink into that black water. You will need something to float on."
                };
            }

            if (_wraithAlive)
            {
                return new ActionResult
                {
                    Message = "When you start to push the raft into the black water, it jolts beneath you as something cold and thin rises from the depth. A pale shape unfolds in the dark, and a mine wraith lunges out of the lake.",
                    Fight = new Opponent
                    {
                        Name = "Mine Wraith",
                        FightingSkill = 14,
                        Health = 20,
                        SuccessEvent = new ActionResult
                        {
                            Message = "The creature dissolves into the water with a hiss of cold air, and the hidden stream is now yours to follow.",
                            Custom = ctx =>
                            {
                                _wraithAlive = false;
                                ctx.Player.FightingSkill += 2;
                            }
                        },
                        FailEvent = new ActionResult
                        {
                            Message = "The mire swallows you whole, and the mine keeps your name in the dark.",
                            GameResult = GameResult.Fail
                        }
                    }
                };
            }

            return new ActionResult
            {
                Message = "You push the raft into the black water and drift across the pool. The pool is bigger than you thought. The barely visible mine walls seems to breathe around you. After a while you reach the other side where a stream carries you toward a hidden opening.",
                MoveToRoomId = "stream way"
            };
        }

        return null;
    }
}
