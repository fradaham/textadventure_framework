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

    public string TitleMusic {get;} = "./GameData/Music/title_bau.mp3";

    public string? MainMusic {get;} = "./GameData/Music/main.mp3";

    public string? DeathMusic {get;} = "./GameData/Music/gameover.mp3";

    public string? SuccessMusic {get;} = "./GameData/Music/success.mp3";

    public string? BattleMusic {get;} = "./GameData/Music/battle.mp3";

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
                    new Grind(),
                    new Skog()
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

    public string About { get; } = @"Detta är ett spel som visar hur NaiTextAdventure-biblioteket kan användas för att skapa enkla textspel. Det har också använts som testspel. Tillkomsten av både spelet och biblioteket styrdes av att min son sa att han ville lära sig att koda, och hans första idé var att göra ett textspel. Dock tyckte han efter en stund att det såg väldans trist ut med de sedvanliga konsoloperationerna. Detta drev mig till att skapa ett exempel på hur ett textspel skulle kunna se ut. Att man kunde göra det lite roligare. Det ena ledde till det andra, och plötsligt satt jag också skapade ett enkelt ramverk för att skapa enkla textspel utefter ett visst mönster. Det måste dock påpekas att ramverket vid skrivande stund lämnar övrigt att önska på många punkter. Det är inte klart på långa vägar, men tillräckligt klart för att börja använda i en första omega-version (för spel på svenska för tillfället).

    'Blajan' är som titeln avslöjar givetvist _enormt_ seriöst menat. Vid sidan av det höga kulturella innehållet, så bör det observeras att implementationen är långt ifrån heltäckande. Vissa föremål och rum har ett brett stöd av kommandon, medans andra endast är basalt implementerade. Dock är det ett spel som man kan klara. Det är heltäckande på så sätt.

    Kodning: Omloppsban-Pan
    Berättelse: Omloppsban-Pan
    Musik: Omloppsban-Pan

    För musiken har jag plockat ut några trackerlåtar från min mapp av ofärdiga trackerlåtar. Jag tyckte att det passade ett demospel som detta. Om jag någonsin gör ett mer seriöst spel ser jag till att använda färdiga låtar. Jag har klippt bort de ofärdiga delarna och låtit den kvarvarande biten av låten tjänstgöra som musik för den del av spelet som jag tyckte att den passade till. Låtarna är av olika ambitionsgrad. Titellåten ligger lågt på den skalan, men jag tyckte att det passade till speltiteln =).
    ";
}
