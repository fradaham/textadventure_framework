using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.Swedish;

public class SwedishInterpreterSettings : IGeneralInterpreterSettings
{
    public IEnumerable<Command> Commands => CommandDefinitions.Commands;

    public string CommandListHelp()
    {
        string helpText = "Tolken stödjer följande kommandon: \n";
        helpText += String.Join(", ", Commands.Select(c => c.Predicate.Imperativ));

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
}