using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.English;

public class EnglishInterpreterSettings : IGeneralInterpreterSettings
{
    public IEnumerable<Command> Commands => CommandDefinitions.Commands;

    public string GeneralHelp()
    {
        string helpText = "The interpreter supports the following commands: \n";
        helpText += String.Join(", ", Commands.Select(c => c.Predicate.Imperativ));

        return helpText;
    }

    public string NoMatchingPredicateFound(string input)
    {
        return $"Cannot find a command matching the start of '{input.Trim()}'";
    }

    public string NoPatternMatchForPredicate(IPredicate predicate, string rest)
    {
        return $"'rest' does not match any known patterns for command '{predicate.Infinitiv}'";
    }
}