using System.Linq;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;
using Nai.TextAdventure.Language.General;
using Nai.TextAdventure.UserInterface;
using Xunit;

namespace NaiTextAdventure.Tests;

public class UserTextTests
{
    [Fact]
    public void EnglishMenuOptionsShouldUseEnglishLabels()
    {
        Assert.Equal("Start game", UserText.StartGame(GameLanguage.English));
        Assert.Equal("About", UserText.AboutOption(GameLanguage.English));
        Assert.Equal("Quit", UserText.QuitOption(GameLanguage.English));
    }

    [Fact]
    public void SwedishMenuOptionsShouldUseSwedishLabels()
    {
        Assert.Equal("Starta spelet", UserText.StartGame(GameLanguage.Swedish));
        Assert.Equal("Om", UserText.AboutOption(GameLanguage.Swedish));
        Assert.Equal("Avsluta", UserText.QuitOption(GameLanguage.Swedish));
    }

    [Fact]
    public void EnglishCommandDefinitionsShouldIncludeAllPredicates()
    {
        var predicates = CommandDefinitions.Commands
            .Select(command => command.Predicate.Imperativ)
            .ToHashSet();

        Assert.Contains("go", predicates);
        Assert.Contains("put", predicates);
        Assert.Contains("pick up", predicates);
        Assert.Contains("open", predicates);
        Assert.Contains("close", predicates);
        Assert.Contains("discard", predicates);
        Assert.Contains("unlock", predicates);
        Assert.Contains("lock", predicates);
        Assert.Contains("look", predicates);
        Assert.Contains("examine", predicates);
        Assert.Contains("throw", predicates);
        Assert.Contains("stab", predicates);
        Assert.Contains("help", predicates);
        Assert.Contains("eat", predicates);
    }
}
