// using System.Collections.Immutable;

// namespace TextAdventure.Language;

// public enum ActionType
// {
//     Go, 
//     Get,
//     Put,
//     Drop,
//     Examine,
//     Eat,
//     Open,
//     Close,
//     Use,
//     Lock,
//     Unlock,
//     Discard,
//     Throw,
//     Sleep
// }


// public class Initiator(InitiatorType type, string value)
// {
//     public required InitiatorType Type { get; init; } = type;

//     public required string Value {get; init;} = value;
// }



// public enum InitiatorType
// {
//     IndirectObject, PlaceAdverbial, MannerAdverbial
// }

// public static class Swedish
// {
//     public static IEnumerable<
//     public static IDictionary<ActionType, Predicate> Predicate = new Dictionary<ActionType, Predicate>
//     {
//         { 
//             ActionType.Go,
//             new Predicate()
//             {
//                 Verb = "gå",
//                 Type = VerbType.Intransitive,
//                 Infinitiv = "gå",
//                 ObjectSeparators = ["till", "över", "på", "under", "i"],
//                 DirectObjectIndicator = ObjectConstants.NotAllowed,
//                 IndirectObjectIndicator = ObjectConstants.Mandatory
//             }
//         },
//         { 
//             ActionType.Sleep,
//             new Predicate()
//             {
//                 Verb = "sov",
//                 Type = VerbType.Intransitive,
//                 Infinitiv = "sova",
//                 ObjectSeparators = ["på", "i", "under"],
//                 DirectObjectIndicator = ObjectConstants.NotAllowed,
//                 IndirectObjectIndicator = ObjectConstants.Optional
//             }
//         },
//         {
//             ActionType.Get, 
//             new Predicate()
//             {
//                 Verb = "Ta",
//                 Type = VerbType.Transitive,
//                 Infinitiv = "ta",
//                 Synonyms = 
//                 [
//                     "Ta upp",
//                     "Plocka upp",
//                 ],
//                 DirectObjectIndicator = ObjectConstants.Mandatory,
//                 IndirectObjectIndicator = ObjectConstants.Optional
//             }
//         },
//         { 
//             ActionType.Put, new Predicate()
//             {
//                 Verb = "Sätt",
//                 Type = VerbType.Transitive,
//                 Infinitiv = "sätta",
//                 ObjectSeparators = ["i", "på", "under", "över"],
//                 DirectObjectIndicator = ObjectConstants.Mandatory,
//                 IndirectObjectIndicator = ObjectConstants.Mandatory
//             }
//         },
//         {
//             ActionType.Drop, new Predicate()
//             {
//                 Verb = "Släpp",
//                 Type = VerbType.Transitive,
//                 Infinitiv = "släppa",
//                 ObjectSeparators = ["i", "på", "under", "över"],
//                 DirectObjectIndicator = ObjectConstants.Mandatory,
//                 IndirectObjectIndicator = ObjectConstants.Optional
//             }
//         },
//         {
//             ActionType.Discard, new Predicate()
//             {
//                 Verb = "Kasta bort",
//                 Infinitiv = "kasta bort",
//                 Type = VerbType.Transitive,
//                 DirectObjectIndicator = ObjectConstants.Mandatory,
//                 IndirectObjectIndicator = ObjectConstants.NotAllowed
//             }
//         },
//         {
//             ActionType.Throw, new Predicate()
//             {
//                 Verb = "Kasta",
//                 Type = VerbType.Transitive,
//                 Infinitiv = "kasta",
//                 ObjectSeparators = ["i", "på", "under", "över"],
//                 DirectObjectIndicator = ObjectConstants.Mandatory,
//                 IndirectObjectIndicator = ObjectConstants.Mandatory
//             }
//         },
//         {
//             ActionType.Examine,
//             new Predicate()
//             {
//                 Verb = "Undersök",
//                 Infinitiv = "undersöka",
//                 Type = VerbType.Transitive, //?
//                 Synonyms = 
//                 [
//                     "Titta",
//                     "Titta på"
//                 ],
//                 ObjectSeparators = ["med", "användandes"],
//                 DirectObjectIndicator = ObjectConstants.Mandatory,
//                 IndirectObjectIndicator = ObjectConstants.Optional
//             }
//         },
//         {
//             ActionType.Eat,
//             new Predicate()
//             {
//                 Verb = "Ät",
//                 Infinitiv = "äta",
//                 Type = VerbType.Transitive,//?
//                 Synonyms = 
//                 [
//                     "ät upp",
//                     "Svulla",
//                     "Mumsa i dig",
//                     "Glufsa i dig"
//                 ],
//                 ObjectSeparators = ["med", "användandes"],
//                 DirectObjectIndicator = ObjectConstants.Mandatory,
//                 IndirectObjectIndicator = ObjectConstants.Optional
//             }
//         },
//         {
//             ActionType.Open,
//             new Predicate()
//             {
//                 Verb = "Öppna",
//                 Infinitiv = "öppna",
//                 Type = VerbType.Transitive,
//                 ObjectSeparators = ["med", "användandes"],
//                 DirectObjectIndicator = ObjectConstants.Mandatory,
//                 IndirectObjectIndicator = ObjectConstants.Optional
//             }
//         },
//         {
//             ActionType.Close,
//             new Predicate()
//             {
//                 Verb = "Stäng",
//                 Infinitiv = "stänga",
//                 Type = VerbType.Transitive,
//                 ObjectSeparators = ["med", "användandes"],
//                 DirectObjectIndicator = ObjectConstants.Mandatory,
//                 IndirectObjectIndicator = ObjectConstants.Optional
//             }
//         },
//         {
//             ActionType.Use,
//             new Predicate()
//             {
//                 Verb = "Använd",
//                 Infinitiv = "använda",
//                 Type = VerbType.Transitive,
//                 DirectObjectIndicator = ObjectConstants.Mandatory,
//                 ObjectSeparators = ["med"],
//                 IndirectObjectIndicator = ObjectConstants.Optional
//             }
//         },
//         {
//             ActionType.Lock,
//             new Predicate()
//             {
//                 Verb = "lås",
//                 Type = VerbType.Transitive,
//                 Infinitiv = "låsa",
//                 ObjectSeparators = ["med", "användandes"],
//                 DirectObjectIndicator = ObjectConstants.Mandatory,
//                 IndirectObjectIndicator = ObjectConstants.Optional
//             }
//         },
//         {
//             ActionType.Unlock,
//             new Predicate()
//             {
//                 Verb = "lås upp",
//                 Type = VerbType.Transitive,
//                 Infinitiv = "låsa upp",
//                 ObjectSeparators = ["med", "användandes"],
//                 DirectObjectIndicator = ObjectConstants.Mandatory,
//                 IndirectObjectIndicator = ObjectConstants.Optional
//             }
//         }
//     };

//     // public static ImmutableList<Predicate> Predicates =
//     // [
//     //   new Predicate()
//     //   {
//     //       Verb = "Gå"
//     //   },
//     //   new Predicate()
//     //   {
//     //       Verb = "Ta",
//     //       Synonyms = 
//     //       [
//     //         "Ta upp",
//     //         "Plocka upp",
//     //       ]
//     //   },
//     //   new Predicate()
//     //   {
//     //       Verb = "Sätt",
//     //   },
//     //   new Predicate()
//     //   {
//     //       Verb = "Släpp",
//     //   },
//     //   new Predicate()
//     //   {
//     //       Verb = "Undersök",
//     //       Synonyms = 
//     //       [
//     //         "Titta"
//     //       ]
//     //   },
//     //   new Predicate()
//     //   {
//     //       Verb = "Ät",
//     //       Synonyms = 
//     //       [
//     //         "Svulla",
//     //         "Mumsa",
//     //         "Glufsa"
//     //       ]
//     //   },
//     //   new Predicate()
//     //   {
//     //       Verb = "Bajsa",
//     //       Synonyms = 
//     //       [
//     //         "Baua",
//     //         "Spritsa"
//     //       ]
//     //   },
//     //   new Predicate()
//     //   {
//     //       Verb = "Öppna",
//     //   },
//     //     new Predicate()
//     //   {
//     //       Verb = "Stäng",
//     //   },  
//     //   new Predicate()
//     //   {
//     //       Verb = "Använd",
//     //   },                                                           
//     // ];
// }