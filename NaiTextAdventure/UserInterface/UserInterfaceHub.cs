using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.UserInterface;

namespace Nai.TextAdventure;

public class UserInterfaceHub
{
    private Context _context;

    private ITextUserInterface _titleUI, _mainUI, _deathUI, _successUI, _battleUI, _aboutUI;

    private IGameSetup _setup;

    public UserInterfaceHub(IGameSetup setup, MainGameEngine engine)
    {
        _setup = setup;
        //World world = setup.World;
        _titleUI = new TitleUI(setup.Title, setup.SubTitle, setup.Creator, setup.TitleMusic);
        _mainUI = new MainUI(engine, setup.MainMusic);
        _deathUI = new DeathUI(setup.DeathComment, setup.DeathMusic);
        _successUI = new SuccessUI(setup.SuccessComment, setup.SuccessMusic);
        _battleUI = new BattleUserInterface(setup.BattleMusic);
        _aboutUI = new AboutUI(setup.About);
        Player player = new Player("DUMMY", 25, 25, 15, 15, 3, setup.World.GetRoom(setup.StartingRoomName)!); //TODO: Think through messy context making this unnecessary
        _context = new Context(setup.World, player);
    }

    public void Run(GameState initState)    
    {
        TuiResult tuiResult = new TuiResult(initState);
        
        while(true)
        {
            if (tuiResult.TargetView == GameState.Title)
            {
                tuiResult = _titleUI.Execute(_context);
                if (tuiResult.TargetView == GameState.Main)
                {
                    World world = _setup.World; //Important to get a new each time a new game is started (important that the GameSetup impl is implementing a get method tha provides a new world object each time)
                    IRoom startingRoom = world.GetRoom(_setup.StartingRoomName)!;
                    Player player = new Player("Torleif", 25, 25, 15, 15, 3, startingRoom); //TODO: An input UI for this
                    //TODO: Think through messy context making this ugly thing unnecessary
                    _context = new Context(_setup.World, player);
                    tuiResult = new TuiResult(GameState.Main, startingRoom.Enter(_context));
                    _mainUI.Reset();
                }
            }
            else if (tuiResult.TargetView == GameState.Main)
            {
                tuiResult = _mainUI.Execute(_context, tuiResult.PassedOnActionResult);
            }
            else if (tuiResult.TargetView == GameState.Quit)
            {
                break;
            }
            else if (tuiResult.TargetView == GameState.Completed)
            {
                tuiResult = _successUI.Execute(_context, tuiResult.PassedOnActionResult);
            }
            else if (tuiResult.TargetView == GameState.Death)
            {
                tuiResult = _deathUI.Execute(_context, tuiResult.PassedOnActionResult);
            }
            else if (tuiResult.TargetView == GameState.Fight)
            {
                tuiResult = _battleUI.Execute(_context, tuiResult.PassedOnActionResult);
                _battleUI.Reset();
            }
            else if (tuiResult.TargetView == GameState.About)
            {
                tuiResult = _aboutUI.Execute(_context, tuiResult.PassedOnActionResult);
            }
        }
    }

    
}

public class TextInputException: Exception
{
    public TextInputException(string message): base(message)
    {}
}