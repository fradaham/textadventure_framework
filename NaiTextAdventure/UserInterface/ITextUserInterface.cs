using Nai.TextAdventure.Concepts;
using Spectre.Console;

namespace Nai.TextAdventure.UserInterface;

public interface ITextUserInterface
{
    TuiResult Execute(Context context, ActionResult? incomingActionResult = null);

    void Reset();
}

public enum GameState
{
    Quit,
    Title, 
    About,
    Main,
    Init, 
    Fight,
    Equip,
    Completed,
    Death,
}

public class TuiResult(GameState targetView)
{
    public GameState TargetView {get;} = targetView;

    //public Context? Context {get; init;}
    public ActionResult? PassedOnActionResult {get; }

    public TuiResult(GameState targetView, ActionResult? actionResult): this(targetView)
    {
        PassedOnActionResult = actionResult;
    }
}