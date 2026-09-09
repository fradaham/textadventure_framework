using System.Text.RegularExpressions;
using TextAdventure.Language.Concepts;

namespace TextAdventure.Language.Swedish
{
    public class SwedishInterpreter: IInterpreter
    {
        public ParsingResult Parse(string input)
        {
            input = input.Trim();
            //First find starting predicate, defining command (match longest possible)
            List<string> predicateVerbs = new();
            foreach (Command command in TextAdventure.Language.Swedish.Definitions.Commands)
            {
                predicateVerbs.AddRange(command.Predicate.GetAllMatchingAlternatives());
            }
            IEnumerable<string> orderedPredicateVerbs = predicateVerbs.OrderByDescending(v => v.Length);
            string? foundMatchingPredicateVerb = orderedPredicateVerbs.FirstOrDefault(v => input.StartsWith(v, StringComparison.InvariantCultureIgnoreCase));

            if (foundMatchingPredicateVerb == null)
            {
                return new ParsingResult()
                {
                    ErrorMessage = $"Kan inte förstå vad du vill göra. Hittar inget matchande kommando för {input.Trim().Split("")[0]}."
                };
            }

            Command matchingCommand = Definitions.Commands.First(command => command.Predicate.Verb.Equals(foundMatchingPredicateVerb) || (command.Predicate.Synonyms?.Any(s => s.Equals(foundMatchingPredicateVerb)) ?? false));
            
            //Try to parse the rest of the input according to command patterns
            string rest = input[foundMatchingPredicateVerb.Length..].Trim();

            GroupCollection? matchingGroups = matchingCommand.MatchSentenceParts(rest);

            if (matchingGroups == null)
            {
                return new ParsingResult()
                {
                    ErrorMessage = $"Jag förstår att du vill {matchingCommand.Predicate.Infinitiv}, men resten passar inget språkligt mönster jag förstår."
                };
            }

            return new ParsingResult()
            {
                Command = matchingCommand,
                SentenceParts = matchingGroups
            };
        }
    }
}
