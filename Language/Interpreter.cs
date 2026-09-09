using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Transactions;
using TextAdventure.Concepts;

namespace TextAdventure.Language;

// public class SwedishInterpreter
// {
//     public static Command Parse(string input, Context context)
//     {
//         string[] parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
//         string whole = String.Join(" ", parts);

//         if (String.IsNullOrEmpty(whole))
//         {
//             return new Command(ErrorType.NoContent, "Va?");
//         }
        
//         KeyValuePair<ActionType, Predicate>? matchingPredicate = null;
//         string? rest = null;
//         (matchingPredicate, rest) = FindPredicate(whole);
//         Predicate predicate;

//         if (String.IsNullOrEmpty(rest))
//         {
//             if (matchingPredicate == null) //This should never happen
//             {
//                 return new Command(ErrorType.General, $"Nu blev något skumt. Jag förstår inte '{whole}'. Det här borde inte hända."); 
//             }
//             predicate = matchingPredicate.Value.Value;
//             if (predicate.Type == VerbType.Transitive)
//             {
//                 return new Command(ErrorType.MissingDirectObject, $"Du kan inte bara {predicate.Infinitiv}. Du måste tala om vad du vill {predicate.Infinitiv}."); 
//             }

//             return new Command(predicate); //The intransitive case
//         }

//         //We need to parse the objects
//         List<(IDenominational, int, string)> foundObjects = FindDenominationals(context, rest);

//         if (matchingPredicate is null)
//         {
//             if (foundObjects.Count() == 0)
//             {
//                 return new Command(ErrorType.General, "Jag förstår inte alls. Varken vad du vill göra eller vad du vill göra det på/med.");
//             }
//             if (foundObjects[0].Item2 == 0)
//             {
//                 return new Command(ErrorType.MissingPredicate, $"Om du vill göra något med {foundObjects[0].Item1.Name} måste du tala om vad du vill göra först i meningen (ett verb). Annars förstår inte jag.");
//             }
//             else
//             {
//                 return new Command(ErrorType.UnknownPredicate, $"Du kan inte '{rest[0..foundObjects[0].Item2]}'. Jag känner tyvärr inte till det verbet. Du får uttrycka på ett annat vis vad du vill göra med {foundObjects[0].Item1.Name}.");
//             }
//         }
        
//         // We have a predicate match and there was a "rest" that needed to be interpreted
//         predicate = matchingPredicate.Value.Value;

//         if (foundObjects.Count() == 0)
//         {
//             if ((predicate.ObjectSeparators ?? []).Count() > 0)
//             {
//                 foreach (string separator in predicate.ObjectSeparators)
//                 {
                    
//                 }
//             }
//         }
        
     
//     }

//     private static Command GetCommandForPredicate(Predicate predicate, string input, Context context)
//     {
//         if ((predicate.ObjectSeparators is null || predicate.ObjectSeparators.Count() == 0) && predicate.IndirectObjectIndicator == ObjectConstants.NotAllowed)
//         {
//             if (String.IsNullOrEmpty(input))
//             {
//                 return new Command(predicate);
//             }
//             else
//             {
//                 IDenominational? directObj = FindDenominational(context, input);
//                 if (directObj == null)
//                 {
//                     return new Command(ErrorType.UnknownDirectObject, $"Du finns ingen '{input}' att {predicate.Infinitiv} här.");
//                 }
//                 else
//                 {
//                     return new Command(predicate, directObj);
//                 }
//             }
//         }
        
//         foreach (string separator in predicate.ObjectSeparators)
//         {
//             if (input.Contains(separator))
//             {
//                 string[] parts = input.Split(separator, StringSplitOptions.TrimEntries);
                
//                 if (parts.Length > 2)
//                 {
//                     return new Command(ErrorType.General, $"Du får bara använda '{separator}' en gång för att ange indirekt objekt, annars förstår jag inte.");
//                 }
                
//                 if (string.IsNullOrEmpty(parts[0]) && predicate.DirectObjectIndicator == ObjectConstants.Mandatory)
//                 {
//                     return new Command(ErrorType.MissingDirectObject, $"Du måste ange vad du vill {predicate.Infinitiv}");
//                 }
//                 if (!string.IsNullOrEmpty(parts[0]) && predicate.DirectObjectIndicator == ObjectConstants.NotAllowed)
//                 {
//                     return new Command(ErrorType.General, $"Du kan inte {predicate.Infinitiv} {parts[0]}. Det är inte en korrekt meningsbyggnad.");
//                 }

//                 if (string.IsNullOrEmpty(parts[1]) && predicate.IndirectObjectIndicator == ObjectConstants.Mandatory)
//                 {
//                     return new Command(ErrorType.MissingIndirectObject, $"Du måste ange vad du vill {predicate.Infinitiv} {parts[0]} {separator}.");
//                 }
//                 if (!string.IsNullOrEmpty(parts[0]) && predicate.DirectObjectIndicator == ObjectConstants.NotAllowed)
//                 {
//                     return new Command(ErrorType.General, $"Du kan inte {predicate.Infinitiv} {parts[0]}. Det är inte en korrekt meningsbyggnad.");
//                 }

//                 IDenominational? directObj = FindDenominational(context, parts[0]);
//                 IDenominational? indirectObj = FindDenominational(context, parts[1]);

//                 if (directObj is null && indirectObj is null)
//                 {
//                     return new Command(ErrorType.UnknownDirectObject, $"Jag ser varken {parts[0]} eller {parts[1]} här." );
//                 }
//                 else if (directObj == null)
//                 {
//                     return new Command(ErrorType.UnknownDirectObject, $"Jag ser inte {parts[0]} här.");
//                 }
//                 else if (indirectObj == null)
//                 {
//                     return new Command(ErrorType.UnknownIndirectObject, $"Jag ser inte {parts[1]} här.");
//                 }
//                 else
//                 {
//                     return new Command(predicate, directObj, indirectObj, separator);
//                 }
//             }
//         }


       

//     }

//     private static (KeyValuePair<ActionType, Predicate>?, string?)  FindPredicate(string input)
//     {
//         foreach(KeyValuePair<ActionType, Predicate> predicate in Swedish.Predicate)
//         {
//             //Match the longest possible part as the predicate. Avoid just "ta" will be matched in ("ta upp blajan").
//             List<string> words = new();
//             words.Add(predicate.Value.Verb);
//             words.AddRange(predicate.Value.Synonyms ?? []);
//             IEnumerable<string> orderedWords = words.OrderByDescending(w => w.Length).ToList();
//             foreach(string word in orderedWords)
//             {
//                 Regex verbRegex = new(@$"^(?<predicate>{predicate.Value.Verb})(?<rest>[\w\s\dåäöÅÄÖ]*)");
//                 Match match = verbRegex.Match(input);
//                 if (match.Success)
//                 {
//                     return (predicate, match.Groups["rest"].ToString());
//                 }
//             }
//             // Regex verbRegex = new(@$"^(?<predicate>{predicate.Value.Verb})(?<rest>[\w\s\dåäöÅÄÖ]*)");
//             // Match match = verbRegex.Match(input);
//             // if (match.Success)
//             // {
//             //     return (predicate, match.Groups["rest"].ToString());
//             // }
//             // else
//             // {
//             //     foreach(string synonym in predicate.Value.Synonyms ?? [])
//             //     {
//             //         Regex synonymRegex = new(@$"^(?<predicate>{synonym})(?<rest>[\w\s\dåäöÅÄÖ]*)");
//             //         Match synonymMatch = synonymRegex.Match(input);
//             //         if (synonymMatch.Success)
//             //         {
//             //             return (predicate, synonymMatch.Groups["rest"].ToString());
//             //         }
//             //     }
//             // }
//         }
//         return (null, input);
        
//         // return Swedish.Predicate.FirstOrDefault(kv => 
//         //     kv.Value.Verb.Equals(input, StringComparison.InvariantCultureIgnoreCase) || 
//         //     (kv.Value.Synonyms?.Any(s => s.Equals(input, StringComparison.InvariantCultureIgnoreCase)) ?? false)).Value;
        
//         // return Swedish.Predicates.FirstOrDefault(p => 
//         //     p.Verb.Equals(input, StringComparison.InvariantCultureIgnoreCase) || 
//         //     (p.Synonyms?.Any(s => s.Equals(input, StringComparison.InvariantCultureIgnoreCase)) ?? false));
//     }

//     public static List<(IDenominational, int, string)> FindDenominationals(Context context, string input)
//     {
//         List<IDenominational> denominationals = new List<IDenominational>();
//         denominationals.AddRange(context.Room.Items);
//         denominationals.AddRange(context.Player.Inventory);
//         denominationals.Add(context.Room);

//         List<(IDenominational denominational, int pos, string name)> foundDenoms = new();

//         foreach(IDenominational denom in denominationals)
//         {
//             //Match the longest possible part as the predicate. Avoid just "ta" will be matched in ("ta upp blajan").
//             List<string> names = new();
//             names.Add(denom.Name);
//             names.AddRange(denom.Synonyms ?? []);
//             IEnumerable<string> orderedNames = names.OrderByDescending(w => w.Length).ToList();
            
//             foreach(string name in orderedNames)
//             {
//                int pos = input.IndexOf(name, StringComparison.InvariantCultureIgnoreCase);
//                 if (pos >= 0)
//                 {
//                     foundDenoms.Add((denom, pos, name));
//                 } 
//             }
//         }

//         List<(IDenominational, int, string)> orderedDenoms = foundDenoms.OrderBy(tuple => tuple.pos).ToList();
        
//         return orderedDenoms;
//     }

//     private static bool MatchesDenominational(IDenominational denom, string name)
//     {
//         List<string> names = new();
//         names.Add(denom.Name);
//         names.AddRange(denom.Synonyms ?? []);

//         return names.Any(n => n.Equals(name, StringComparison.InvariantCultureIgnoreCase));
//     }

//     private static IDenominational? FindDenominational(Context context, string name)
//     {
//         IEnumerable<IDenominational> denominationals = context.GetAllDenominationals();

//         return denominationals.FirstOrDefault(d => MatchesDenominational(d, name));
//     }
// }



