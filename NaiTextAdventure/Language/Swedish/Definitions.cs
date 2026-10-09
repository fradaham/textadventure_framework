using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.Swedish
{
    public static class Predicates
    {
        public static readonly Predicate Gå = new()
        {
            Infinitiv = "gå",
            Imperativ = "gå",
            Synonyms = ["knalla"]
        };

        public static readonly Predicate Lägg = new()
        {
            Infinitiv = "lägga",
            Imperativ = "lägg",
        };

        public static readonly Predicate Ta = new()
        {
            Infinitiv = "ta",
            Synonyms = ["tag", "ta upp", "tag upp", "plocka upp"],
            Imperativ = "ta",
        };

        public static readonly Predicate Öppna = new()
        {
            Infinitiv = "öppna",
            Imperativ = "öppna",
        };

        public static readonly Predicate Stäng = new Predicate()
        {
            Infinitiv = "stänga",
            Imperativ = "stäng",
        };

        public static readonly Predicate Sätt = new Predicate()
        {
            Infinitiv = "sätta",
            Imperativ = "sätt",
        };

        public static readonly Predicate Släpp = new Predicate()
        {
            Infinitiv = "släppa",
            Imperativ = "släpp",
        };

        public static readonly Predicate Lås_upp = new Predicate()
        {
            Infinitiv = "låsa upp",
            Imperativ = "lås upp",
        };

        public static readonly Predicate Lås = new Predicate()
        {
            Infinitiv = "låsa",
            Imperativ = "lås",
        };

        public static readonly Predicate Titta = new Predicate()
        {
            Infinitiv = "titta",
            Imperativ = "titta"
        };

        public static readonly Predicate Undersök = new Predicate()
        {
            Infinitiv = "undersöka",
            Imperativ = "undersök"
        };

        public static readonly Predicate Torka = new Predicate()
        {
            Infinitiv = "torka",
            Imperativ = "torka"
        };

        public static readonly Predicate Torka_av = new Predicate()
        {
            Infinitiv = "torka av",
            Imperativ = "torka av"
        };

        public static readonly Predicate Släng = new Predicate()
        {
            Infinitiv = "slänga",
            Imperativ = "släng",        
        };

        public static readonly Predicate Stick = new Predicate()
        {
            Infinitiv = "sticka",
            Imperativ = "stick",
        };

        public static readonly Predicate Hjälp = new Predicate()
        {
            Infinitiv = "hjälpa",
            Imperativ = "hjälp",
        };

        public static readonly Predicate Ät = new Predicate()
        {
            Infinitiv = "äta",
            Imperativ = "ät",
        };
    }

    public static class Definitions
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
                ]

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
}
