namespace TextAdventure.Language.Concepts;

public interface INoun
{
    string Name {get;}

    string DefiniteForm { get;}

    string Pronomen { get; }

    bool IsMatch(string input);

}