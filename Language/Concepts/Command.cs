using System.Text.RegularExpressions;

namespace TextAdventure.Language.Concepts;

public class Command
{
    public required Predicate Predicate { get; init; }

    public required IEnumerable<Regex> Patterns { get; init; }

    public GroupCollection? MatchSentenceParts(string input)
    {
        foreach(Regex regex in Patterns)
        {
            Match match = regex.Match(input);
            if (match.Success)
            {
                return match.Groups;
            }
        }

        return null;
    }
}