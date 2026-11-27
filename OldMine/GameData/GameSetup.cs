using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Language.Concepts;
using OldMine.GameData.Rooms;

namespace OldMine.GameData;

public class GameSetup: IGameSetup
{
    public GameLanguage Language { get; } = GameLanguage.English;

    public string Title {get; } = "THE OLD MINE";

    public string SubTitle { get;} = "A journey into the darkness beneath the hill";

    public string QuitPhrase {get;} = "Thanks for playing!";

    public string Creator {get;} = "Omloppsban-Pan";

    public string DeathComment {get;} = "The mine keeps your name in the dark.";

    public string SuccessComment {get;} = "You made it out alive, though the old mine still breathes behind you.";

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
                    new ForestTrail(),
                    new MineEntrance(),
                    new LowerGallery(),
                    new ToolRoom(),
                    new CollapsedChamber(),
                    new BlackWaterChamber(),
                    new StreamWay()
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

    public string StartingRoomName {get;} = "forest trail";

    public string About { get; } = @"The Old Mine is a short survival-horror text adventure built with the NaiTextAdventure library.

The purpose of the game is mainly to test and demonstrate the english support in the NaiTextAdventure library, and to provide a simple example of how to use the library to create a text adventure game. 

The game was created using AI, against my own better judgement, and is not intended to be a serious work of fiction. I have corrected the most serious flaws in the game logic, but the game still leaves much to be desired. One day I might make a better game, since I still like the setting and the idea of a short survival-horror text adventure in a mine. Or maybe you can?";
}
