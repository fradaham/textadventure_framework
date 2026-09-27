using Spectre.Console;
using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.UserInterface;

namespace Nai.TextAdventure;

public class UserInterfaceHub
{
    private World _world;

    private Player _player;

    private IInterpreter _interpreter;

    private ITextUserInterface _titleUI, _mainUI, _deathUI, _successUI, _battleUI;

    private IGameSetup _setup;

    public UserInterfaceHub(IGameSetup setup, MainGameEngine engine)
    {
        _setup = setup;
        _world = setup.World;
        _interpreter = setup.MainInterpreter;
        _titleUI = new TitleUI(setup.Title, setup.SubTitle, setup.Creator, setup.TitleMusic);
        _mainUI = new MainUI(engine, setup.MainMusic);
        _deathUI = new DeathUI(setup.DeathComment, setup.DeathMusic);
        _successUI = new SuccessUI(setup.SuccessComment, setup.SuccessMusic);
        _battleUI = new BattleUserInterface(setup.BattleMusic);
        _player = new Player("DUMMY", 25, 25, 15, 15, 3, setup.World.GetRoom(setup.StartingRoomName)!); //TODO: Think through messy context making this unnecessary
    }

    public void Run(GameState initState)    
    {
        TuiResult tuiResult = new TuiResult(initState);
        
        while(true)
        {
            if (tuiResult.TargetView == GameState.Title)
            {
                tuiResult = _titleUI.Execute(new Context(_world, _player));
                if (tuiResult.TargetView == GameState.Main)
                {
                    _world = _setup.World; //Important to get a new each time a new game is started (important that the GameSetup impl is implementing a get method tha provides a new world object each time)
                    IRoom startingRoom = _world.GetRoom(_setup.StartingRoomName)!;
                    _player = new Player("Torleif", 25, 25, 15, 15, 3, startingRoom); //TODO: An input UI for this
                    //TODO: Think through messy context making this ugly thing unnecessary
                    tuiResult = new TuiResult(GameState.Main, new Context(_world, _player, startingRoom.Enter(new Context(_world, _player))));
                    _mainUI.Reset();
                }
            }
            else if (tuiResult.TargetView == GameState.Main)
            {
                tuiResult = _mainUI.Execute(new Context(_world, _player!, tuiResult.Context?.ActionResult));
            }
            else if (tuiResult.TargetView == GameState.Quit)
            {
                break;
            }
            else if (tuiResult.TargetView == GameState.Completed)
            {
                tuiResult = _successUI.Execute(new Context(_world, _player!, tuiResult.Context?.ActionResult));
            }
            else if (tuiResult.TargetView == GameState.Death)
            {
                tuiResult = _deathUI.Execute(new Context(_world, _player!, tuiResult.Context?.ActionResult));
            }
            else if (tuiResult.TargetView == GameState.Fight)
            {
                tuiResult = _battleUI.Execute(new Context(_world, _player!, tuiResult.Context?.ActionResult));
                _battleUI.Reset();
            }
        }
    }

    
}

public class TextInputException: Exception
{
    public TextInputException(string message): base(message)
    {}
}