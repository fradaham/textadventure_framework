using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.Swedish;

 public static class Predicates
{
    public static readonly Predicate Gå = new()
    {
        Infinitiv = "gå",
        Imperativ = "gå",
        Synonyms = ["knalla"],
        IsMovement = true
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

    public static readonly Predicate Stäng = new()
    {
        Infinitiv = "stänga",
        Imperativ = "stäng",
    };

    public static readonly Predicate Sätt = new()
    {
        Infinitiv = "sätta",
        Imperativ = "sätt",
    };

    public static readonly Predicate Släpp = new()
    {
        Infinitiv = "släppa",
        Imperativ = "släpp",
    };

    public static readonly Predicate Lås_upp = new()
    {
        Infinitiv = "låsa upp",
        Imperativ = "lås upp",
    };

    public static readonly Predicate Lås = new()
    {
        Infinitiv = "låsa",
        Imperativ = "lås",
    };

    public static readonly Predicate Titta = new()
    {
        Infinitiv = "titta",
        Imperativ = "titta"
    };

    public static readonly Predicate Undersök = new()
    {
        Infinitiv = "undersöka",
        Imperativ = "undersök"
    };

    public static readonly Predicate Torka = new()
    {
        Infinitiv = "torka",
        Imperativ = "torka"
    };

    public static readonly Predicate Torka_av = new()
    {
        Infinitiv = "torka av",
        Imperativ = "torka av"
    };

    public static readonly Predicate Släng = new()
    {
        Infinitiv = "slänga",
        Imperativ = "släng",        
    };

    public static readonly Predicate Stick = new()
    {
        Infinitiv = "sticka",
        Imperativ = "stick",
    };

    public static readonly Predicate Hjälp = new()
    {
        Infinitiv = "hjälpa",
        Imperativ = "hjälp",
    };

    public static readonly Predicate Ät = new()
    {
        Infinitiv = "äta",
        Imperativ = "ät",
    };
}