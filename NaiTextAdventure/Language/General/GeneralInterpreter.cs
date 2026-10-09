using System.Text.RegularExpressions;
using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.General;

public class GeneralInterpreter: IInterpreter
{
    private readonly IGeneralInterpreterSettings _settings;
    public GeneralInterpreter(IGeneralInterpreterSettings settings)
    {
        _settings = settings;
    }
    
    public ParsingResult Parse(string input)
    {
        input = input.Trim();
        //First find starting predicate, defining command (match longest possible)
        List<string> predicates = new();
        foreach (Command command in _settings.Commands)
        {
            predicates.AddRange(GetCommandAlternatives(command.Predicate));
        }
        IEnumerable<string> orderedPredicates = predicates.OrderByDescending(v => v.Length);
        string? foundMatchingPredicate = orderedPredicates.FirstOrDefault(v => input.StartsWith(v + ' ', StringComparison.InvariantCultureIgnoreCase) || input.Equals(v, StringComparison.InvariantCultureIgnoreCase));

        if (foundMatchingPredicate == null)
        {
            return new ParsingResult()
            {
                ErrorMessage = _settings.NoMatchingPredicateFound(input)
            };
        }

        Command matchingCommand = _settings.Commands.First(command => command.Predicate.Imperativ.Equals(foundMatchingPredicate) || (command.Predicate.Synonyms?.Any(s => s.Equals(foundMatchingPredicate)) ?? false));
        
        //Try to parse the rest of the input according to command patterns
        string rest = input[foundMatchingPredicate.Length..].Trim();

        GroupCollection? matchingGroups = matchingCommand.MatchSentenceParts(rest);

        if (matchingGroups == null)
        {
            return new ParsingResult()
            {
                ErrorMessage = _settings.NoPatternMatchForPredicate(matchingCommand.Predicate, rest)
            };
        }

        return new ParsingResult()
        {
            Command = matchingCommand,
            SentenceParts = matchingGroups
        };
    }

    public string Help()
    {
        return _settings.CommandListHelp();
    }

    public string? Help(string directObject)
    {
        foreach(Command command in _settings.Commands)
        {
            IEnumerable<string> predicateAlternatives = GetCommandAlternatives(command.Predicate);
            string? matchingPredicateStr = predicateAlternatives.FirstOrDefault(p => p.Equals(directObject, StringComparison.InvariantCultureIgnoreCase));
            if (matchingPredicateStr != null)
            {
                string helpText = String.Empty;
                foreach (Regex pattern in command.Patterns)
                {
                    string patternText = matchingPredicateStr + " " + String.Join(' ', pattern.GetGroupNames()) + "\n";
                    helpText += matchingPredicateStr + " " + pattern.ToString().Replace("^", "") + "\n";
                }
                return helpText;
            }
        }

        return null;
    }

    private IEnumerable<string> GetCommandAlternatives(IPredicate predicate)
    {
        return [predicate.Imperativ, ..predicate.Synonyms ?? []];
    }
}

