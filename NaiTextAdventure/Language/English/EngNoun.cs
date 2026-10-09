using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.English;

public class EngNoun(string name): INoun
{
    public string Name {get;} = name;

    public string DefiniteForm { get { return "the" + Name;} } 

    public string Pronomen => "it";

    public bool IsMatch(string input)
    {
        return input.Equals(Name, StringComparison.InvariantCultureIgnoreCase) || input.Equals(DefiniteForm, StringComparison.InvariantCultureIgnoreCase);
    }
}