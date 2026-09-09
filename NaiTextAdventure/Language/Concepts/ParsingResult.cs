using System.Text.RegularExpressions;

namespace Nai.TextAdventure.Language.Concepts
{
    public class ParsingResult
    {
        public string? ErrorMessage { get; init;}

        public Command? Command { get; init; }

        public GroupCollection? SentenceParts { get; init; }
    }
}