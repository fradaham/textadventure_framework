using System.Text.RegularExpressions;

namespace Nai.TextAdventure.Language.Concepts
{
    public interface IInterpreter
    {
        ParsingResult Parse(string input);

        string Help();

        string? Help(string predicate);
    }
}

