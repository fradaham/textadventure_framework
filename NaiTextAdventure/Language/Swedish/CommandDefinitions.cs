using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.Swedish;

public static class CommandDefinitions
{
    public static ImmutableArray<Command> Commands = 
    [
        new Command()
        {
            Predicate = Predicates.Gå,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>i|på|under|genom|längs) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                new Regex(@$"^(?<{SentenceParts.PlaceAdverbialInit}>i|på|under|till|mot|genom|längs) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)"),
                new Regex(@$"^(?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)")
            ],
        },
        new Command()
        {
            Predicate = Predicates.Lägg,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Ta,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under|från) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Öppna,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Stäng,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Sätt,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Släpp,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Lås_upp,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Lås,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Titta,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.PlaceAdverbialInit}>i|på|under|till|mot) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)"),
                new Regex(@$"^(?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)"),
                new Regex(@$"^")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Undersök,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)"),
            ]
        },
        new Command()
        {
            Predicate = Predicates.Torka,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
            ]
        },
        new Command()
        {
            Predicate = Predicates.Torka_av,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
            ]
        },
        new Command()
        {
            Predicate = Predicates.Släng,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Stick,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Hjälp,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)"),
                new Regex(@$"^")
            ]
        },
        new Command()
        {
            Predicate = Predicates.Ät,
            Patterns =
            [
                new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)"),
            ]
        }
        
    ];
}

