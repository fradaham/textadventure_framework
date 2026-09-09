using TextAdventure.Concepts;
using TextAdventure.GameData.Items;
using TextAdventure.GameData.Rooms;
using TextAdventure.Language.Concepts;
using TextAdventure.Language.Swedish;

namespace TextAdventure.GameData;

public class GameSetup: IGameSetup
{
    public string Title {get; } = "BLAJAN";

    public string SubTitle { get;} = "Demo: Ett brunt retroäventyr skapat med en helt vanlig (?) människohjärna";

    public string QuitPhrase {get;} = "Tack för att du spelade! Ta nu och gå och lär dig något på riktigt.";

    public string Creator {get;} = "AntiAI-spel";

    public string DeathComment {get;} = "Hur känns det att förlora mot en blaja?";

    public string SuccessComment {get;} = "Du är smartare än en blaja - det finns hopp för mänskligheten!";

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
