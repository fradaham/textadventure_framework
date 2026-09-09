using System.ComponentModel.DataAnnotations;
using TextAdventure.Language.Concepts;

namespace TextAdventure.Language.Swedish;

public class SweNoun(string name, Genus genus): INoun
{
    public string Name {get;} = name;

    public string DefiniteForm { get { return Name + GetSuffix();} } 

    public string Pronomen 
    { 
        get 
        { 
            return genus switch 
            { 
                Genus.Utrum => "den", 
                Genus.Neutrum => "det", 
                _ => throw new NotSupportedException($"Inget matchande pronomen för {Name}")
            };
        }
    }

    public bool IsMatch(string input)
    {
        return input.Equals(Name, StringComparison.InvariantCultureIgnoreCase) || input.Equals(DefiniteForm, StringComparison.InvariantCultureIgnoreCase);
    }

    private string GetSuffix()
    {
        string suffix = genus switch
        {
            Genus.Utrum when (Name.EndsWith('l') || "aouåeiyäö".Contains(Name[(Name.Length - 1)])) => "n",
            Genus.Utrum => "en",
            Genus.Neutrum when "aouåeiyäö".Contains(Name[(Name.Length - 1)]) => "t",
            Genus.Neutrum => "et",
            Genus.None => string.Empty,
            _ => throw new NotImplementedException($"Cannot match what suffix to give {Name}")
        };

        return suffix;
    }

}