using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.UserInterface;
using Xunit;

namespace NaiTextAdventure.Tests;

public class UiTextTests
{
    [Fact]
    public void EnglishMenuOptionsShouldUseEnglishLabels()
    {
        Assert.Equal("Start game", UiText.StartGame(GameLanguage.English));
        Assert.Equal("About", UiText.AboutOption(GameLanguage.English));
        Assert.Equal("Quit", UiText.QuitOption(GameLanguage.English));
    }

    [Fact]
    public void SwedishMenuOptionsShouldUseSwedishLabels()
    {
        Assert.Equal("Starta spelet", UiText.StartGame(GameLanguage.Swedish));
        Assert.Equal("Om", UiText.AboutOption(GameLanguage.Swedish));
        Assert.Equal("Avsluta", UiText.QuitOption(GameLanguage.Swedish));
    }
}
