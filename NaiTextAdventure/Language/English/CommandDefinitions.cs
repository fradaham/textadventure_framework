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
                new Regex(@$"^(?<{SentenceParts.PlaceAdverbialInit}>in|on|to|towards|through|along|into) (?<{SentenceParts.PlaceAdverbial}>[a-z ]+)"),
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
        new Command()
        {
            Predicate = Predicates.Open,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+) (?<{SentenceParts.MannerAdverbialInit}>with|using) (?<{SentenceParts.MannerAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Close,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+) (?<{SentenceParts.MannerAdverbialInit}>with|using) (?<{SentenceParts.MannerAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Discard,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+) (?<{SentenceParts.PlaceAdverbialInit}>in|on|under|into|onto) (?<{SentenceParts.PlaceAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Unlock,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+) (?<{SentenceParts.MannerAdverbialInit}>with|using) (?<{SentenceParts.MannerAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Lock,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+) (?<{SentenceParts.MannerAdverbialInit}>with|using) (?<{SentenceParts.MannerAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Light,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+) (?<{SentenceParts.MannerAdverbialInit}>with|using) (?<{SentenceParts.MannerAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Look,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.PlaceAdverbialInit}>in|on|under|at|around|through|behind|beside) (?<{SentenceParts.PlaceAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.PlaceAdverbial}>[a-z ]+)"),
                new Regex(@$"^")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Examine,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+) (?<{SentenceParts.MannerAdverbialInit}>with|using) (?<{SentenceParts.MannerAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Throw,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+) (?<{SentenceParts.PlaceAdverbialInit}>at|in|on|under|towards|through) (?<{SentenceParts.PlaceAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+) (?<{SentenceParts.MannerAdverbialInit}>with|using) (?<{SentenceParts.MannerAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Stab,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+) (?<{SentenceParts.MannerAdverbialInit}>with|using) (?<{SentenceParts.MannerAdverbial}>[a-z ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Help,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+)"),
                new Regex(@$"^")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Eat,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-z ]+)")
            ]
        },
    ];
}

