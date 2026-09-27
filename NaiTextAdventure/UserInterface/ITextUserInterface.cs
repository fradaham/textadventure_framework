using Nai.TextAdventure.Concepts;
using Spectre.Console;

namespace Nai.TextAdventure.UserInterface;

public interface ITextUserInterface
{
    TuiResult Execute(Context context);

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

    public Context? Context {get; init;}

    public TuiResult(GameState targetView, Context context): this(targetView)
    {
        Context = context;
    }
}