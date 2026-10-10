using System.Text.RegularExpressions;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.General;

namespace Nai.TextAdventure.Language.Swedish;

public class SwedishInterpreterSettings : IGeneralInterpreterSettings
{
    public IEnumerable<Command> Commands => CommandDefinitions.Commands;

    public string CommandListHelp()
    {
        string helpText = "Tolken stödjer följande kommandon: \n";
        helpText += String.Join(", ", Commands.Select(c => c.Predicate.Imperativ));
        helpText += "\n\nSkriv 'hjälp [kommando]' för att se vilka mönster ett visst kommando stödjer. Observera att även om tolken lyckas tolka kommandot, måste rum och föremål ha stöd för det du vill göra.";

        return helpText;
    }

    public string NoMatchingPredicateFound(string input)
    {
        return $"Kan inte förstå vad du vill göra. Hittar inget matchande kommando för {input.Trim().Split("")[0]}.";
    }

    public string NoPatternMatchForPredicate(IPredicate predicate, string rest)
    {
        return $"Jag förstår att du vill {predicate.Infinitiv}, men resten passar inget språkligt mönster jag förstår.";
    }

    public string CommandHelp(Command command)
    {
        string helpText = $"Kommandot '{command.Predicate.Imperativ}' stödjer följande mönster: \n\n";
        foreach (Regex pattern in command.Patterns)
        {
            //string patternText = command.Predicate.Imperativ + " " + String.Join(' ', pattern.GetGroupNames()) + "\n";
            helpText += command.Predicate.Imperativ + " " + pattern.ToString().Replace("^", "") + "\n";
        }
        return helpText;
    }
}