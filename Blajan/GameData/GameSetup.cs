using Nai.TextAdventure.Concepts;
using Blajan.GameData.Items;
using Blajan.GameData.Rooms;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Blajan.GameData;

public class GameSetup: IGameSetup
{
    public string Title {get; } = "BLAJAN";

    public string SubTitle { get;} = "Demo: Ett brunt retroäventyr skapat med en helt vanlig (?) människohjärna";

    public string QuitPhrase {get;} = "Tack för att du spelade!";

    public string Creator {get;} = "Omloppsban-Pan";

    public string DeathComment {get;} = "Hur känns det att förlora mot en blaja?";

    public string SuccessComment {get;} = "Du är smartare än en blaja - ännu finns det hopp för mänskligheten!";

    public World World 
    { 
        get
        {
            return new World()
            {
                Rooms =
                [
                    new Kallare(),
                    new Tradgard(),
                ],
                UnassignedItems =
                [
                    
                ]
            };
        }
    } 

    public IEnumerable<IEntity> InitialPlayerInventory {get;} =
    [
    ];

    public string StartingRoomName {get;} = "källare";

    public IInterpreter MainInterpreter { get; } = new SwedishInterpreter();
}
