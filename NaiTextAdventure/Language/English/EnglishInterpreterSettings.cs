using System.Text.RegularExpressions;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.General;

namespace Nai.TextAdventure.Language.English;

public class EnglishInterpreterSettings : IGeneralInterpreterSettings
{
    public IEnumerable<Command> Commands => CommandDefinitions.Commands;

    public string CommandListHelp()
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

    public string CommandHelp(Command command)
    {
        string helpText = $"The command '{command.Predicate.Imperativ}' supports the following patterns: \n\n";
        foreach (Regex pattern in command.Patterns)
        {
            //string patternText = command.Predicate.Imperativ + " " + String.Join(' ', pattern.GetGroupNames()) + "\n";
            helpText += command.Predicate.Imperativ + " " + pattern.ToString().Replace("^", "") + "\n";
        }
        return helpText;
    }
}