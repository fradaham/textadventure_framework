using Nai.TextAdventure.Concepts;
using Spectre.Console;

namespace Nai.TextAdventure.UserInterface;

internal interface IView
{
    Layout Layout { get; }
    void Update();

    void UserInput(ConsoleKeyInfo key);

    ViewExitData Execute();

}

internal class ViewExitData
{
    internal required IView FromView { get; init;} 
    internal IView? ToView { get; init;} 
}

public interface ITextUserInterface
{
    TuiResult Execute(Context context);
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