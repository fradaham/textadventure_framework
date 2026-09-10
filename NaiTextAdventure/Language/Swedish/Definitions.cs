using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.Swedish
{
    public static class Verbs
    {
        public const string Gå = "gå";
        public const string Ta = "ta";
        public const string Lägg = "lägg";
        public const string Sätt = "sätt";
        public const string Släng = "släng";
        public const string Släpp = "släpp";
        public const string Undersök = "undersök";
        public const string Stäng = "stäng";
        public const string Öppna = "öppna";
        public const string Lås = "lås";
        public const string Lås_upp = "lås upp";
        public const string Titta = "titta";
        public const string Torka = "torka";
        public const string Torka_av = "torka av";
    }

    public static class Definitions
    {
        public static ImmutableArray<Command> Commands = 
        [
            new Command()
            {
                Predicate = new Predicate()
                {
                    Infinitiv = "gå",
                    Verb = Verbs.Gå,
                    Synonyms = ["knalla"]
                },
                Patterns =
                [
                    new Regex(@$"^(?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>i|på|under|genom) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                    new Regex(@$"^(?<{SentenceParts.PlaceAdverbialInit}>i|på|under|till|mot|genom) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)"),
                    new Regex(@$"^(?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)")
                ]

            },
            new Command()
            {
                Predicate = new Predicate()
                {
                    Infinitiv = "lägga",
                    Verb = Verbs.Lägg,
                },
                Patterns =
                [
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)")
                ]
            },
            new Command()
            {
                Predicate = new Predicate()
                {
                    Infinitiv = "ta",
                    Synonyms = ["tag", "ta upp", "tag upp", "plocka upp"],
                    Verb = Verbs.Ta,
                },
                Patterns =
                [
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under|från) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)"),
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
                ]
            },
            new Command()
            {
                Predicate = new Predicate()
                {
                    Infinitiv = "öppna",
                    Verb = Verbs.Öppna,
                },
                Patterns =
                [
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
                ]
            },
            new Command()
            {
                Predicate = new Predicate()
                {
                    Infinitiv = "stänga",
                    Verb = Verbs.Stäng,
                },
                Patterns =
                [
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
                ]
            },
            new Command()
            {
                Predicate = new Predicate()
                {
                    Infinitiv = "sätta",
                    Verb = Verbs.Sätt,
                },
                Patterns =
                [
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)")
                ]
            },
            new Command()
            {
                Predicate = new Predicate()
                {
                    Infinitiv = "släppa",
                    Verb = Verbs.Släpp,
                },
                Patterns =
                [
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.PlaceAdverbialInit}>i|på|under) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)"),
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
                ]
            },
            new Command()
            {
                Predicate = new Predicate()
                {
                    Infinitiv = "låsa upp",
                    Verb = Verbs.Lås_upp,
                },
                Patterns =
                [
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
                ]
            },
            new Command()
            {
                Predicate = new Predicate()
                {
                    Infinitiv = "låsa",
                    Verb = Verbs.Lås,
                },
                Patterns =
                [
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)")
                ]
            },
            new Command()
            {
                Predicate = new Predicate()
                {
                    Infinitiv = "titta",
                    Verb = Verbs.Titta,
                },
                Patterns =
                [
                    new Regex(@$"^(?<{SentenceParts.PlaceAdverbialInit}>i|på|under|till|mot) (?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)"),
                    new Regex(@$"^(?<{SentenceParts.PlaceAdverbial}>[a-zåäö ]+)"),
                    new Regex(@$"^")
                ]
            },
            new Command()
            {
                Predicate = new Predicate()
                {
                    Infinitiv = "undersöka",
                    Verb = Verbs.Undersök,
                },
                Patterns =
                [
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+)"),
                ]
            },
            new Command()
            {
                Predicate = new Predicate()
                {
                    Infinitiv = "torka",
                    Verb = Verbs.Torka,
                },
                Patterns =
                [
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                ]
            },
            new Command()
            {
                Predicate = new Predicate()
                {
                    Infinitiv = "torka av",
                    Verb = Verbs.Torka_av,
                },
                Patterns =
                [
                    new Regex(@$"^(?<{SentenceParts.DirectObject}>[a-zåäö ]+) (?<{SentenceParts.MannerAdverbialInit}>med) (?<{SentenceParts.MannerAdverbial}>[a-zåäö ]+)"),
                ]
            },
            
        ];
    }
}
