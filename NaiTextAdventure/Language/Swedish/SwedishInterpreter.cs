// using System.Text.RegularExpressions;
// using Nai.TextAdventure.Language.Concepts;

// namespace Nai.TextAdventure.Language.Swedish;

// public class SwedishInterpreter: IInterpreter
// {
//     public ParsingResult Parse(string input)
//     {
//         input = input.Trim();
//         //First find starting predicate, defining command (match longest possible)
//         List<string> predicates = new();
//         foreach (Command command in CommandDefinitions.Commands)
//         {
//             predicates.AddRange(GetCommandAlternatives(command.Predicate));
//         }
//         IEnumerable<string> orderedPredicates = predicates.OrderByDescending(v => v.Length);
//         string? foundMatchingPredicate = orderedPredicates.FirstOrDefault(v => input.StartsWith(v + ' ', StringComparison.InvariantCultureIgnoreCase) || input.Equals(v, StringComparison.InvariantCultureIgnoreCase));

//         if (foundMatchingPredicate == null)
//         {
//             return new ParsingResult()
//             {
//                 ErrorMessage = $"Kan inte förstå vad du vill göra. Hittar inget matchande kommando för {input.Trim().Split("")[0]}."
//             };
//         }

//         Command matchingCommand = CommandDefinitions.Commands.First(command => command.Predicate.Imperativ.Equals(foundMatchingPredicate) || (command.Predicate.Synonyms?.Any(s => s.Equals(foundMatchingPredicate)) ?? false));
        
//         //Try to parse the rest of the input according to command patterns
//         string rest = input[foundMatchingPredicate.Length..].Trim();

//         GroupCollection? matchingGroups = matchingCommand.MatchSentenceParts(rest);

//         if (matchingGroups == null)
//         {
//             return new ParsingResult()
//             {
//                 ErrorMessage = $"Jag förstår att du vill {matchingCommand.Predicate.Infinitiv}, men resten passar inget språkligt mönster jag förstår."
//             };
//         }

//         return new ParsingResult()
//         {
//             Command = matchingCommand,
//             SentenceParts = matchingGroups
//         };
//     }

//     public string Help()
//     {
//         string helpText = "Tolken stödjer följande kommandon: \n";
//         helpText += String.Join(", ", CommandDefinitions.Commands.Select(c => c.Predicate.Imperativ));

//         return helpText;
//     }

//     private IEnumerable<string> GetCommandAlternatives(IPredicate predicate)
//     {
//         return [predicate.Imperativ, ..predicate.Synonyms ?? []];
//     }
// }

