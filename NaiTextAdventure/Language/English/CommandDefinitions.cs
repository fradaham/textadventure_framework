using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.English;

public static class CommandDefinitions
{
    public static ImmutableArray<Command> Commands = 
    [
        new Command()
        {
            Predicate = Predicates.Go,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.PlaceAdverbialInit}>in|on|to|towards|through|along) (?<{SentenceParts.PlaceAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.PlaceAdverbial}>[a-z ]+)")
            ],
        },
        new Command()
        {
            Predicate = Predicates.Put,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+) (?<{SentenceParts.PlaceAdverbialInit}>in|on) (?<{SentenceParts.PlaceAdverbial}>[a-z ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Pick_up,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+) (?<{SentenceParts.PlaceAdverbialInit}>in|on|from) (?<{SentenceParts.PlaceAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+) (?<{SentenceParts.MannerAdverbialInit}>with|using) (?<{SentenceParts.MannerAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+)")
            ]
        },
        // new Command()
        // {
        //     Predicate = Predicates.Öppna,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
        //     ]
        // },
        // new Command()
        // {
        //     Predicate = Predicates.Stäng,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
        //     ]
        // },
        // new Command()
        // {
        //     Predicate = Predicates.Sätt,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)")
        //     ]
        // },
        // new Command()
        // {
        //     Predicate = Predicates.Släpp,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)"),
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
        //     ]
        // },
        // new Command()
        // {
        //     Predicate = Predicates.Lås_upp,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
        //     ]
        // },
        // new Command()
        // {
        //     Predicate = Predicates.Lås,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
        //     ]
        // },
        // new Command()
        // {
        //     Predicate = Predicates.Titta,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.PlaceAdverbialInit}>i|på|under|till|mot) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)"),
        //         new Regex(@$"^(?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)"),
        //         new Regex(@$"^")
        //     ]
        // },
        // new Command()
        // {
        //     Predicate = Predicates.Undersök,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)"),
        //     ]
        // },
        // new Command()
        // {
        //     Predicate = Predicates.Torka,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
        //     ]
        // },
        // new Command()
        // {
        //     Predicate = Predicates.Torka_av,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
        //     ]
        // },
        // new Command()
        // {
        //     Predicate = Predicates.Släng,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)")
        //     ]
        // },
        // new Command()
        // {
        //     Predicate = Predicates.Stick,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)")
        //     ]
        // },
        // new Command()
        // {
        //     Predicate = Predicates.Hjälp,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)"),
        //         new Regex(@$"^")
        //     ]
        // },
        // new Command()
        // {
        //     Predicate = Predicates.Ät,
        //     Patterns =
        //     [
        //         new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)"),
        //     ]
        // }
        
    ];
}

