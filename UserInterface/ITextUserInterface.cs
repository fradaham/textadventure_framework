using Spectre.Console;

namespace TextAdventure.UserInterface;

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
    GameState Execute();
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