using System.Data.SqlTypes;
using System.Text;
using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.Swedish;

public static class Utils
{
    // public static string GetSuffix(IDenominational denom, string name)
    // {
    //     string suffix = denom.Genus switch
    //     {
    //         Genus.Utrum when (name.EndsWith('l') || "aouåeiyäö".Contains(name[(name.Length - 1)])) => "n",
    //         Genus.Utrum => "en",
    //         Genus.Neutrum when "aouåeiyäö".Contains(name[(name.Length - 1)]) => "t",
    //         Genus.Neutrum => "et",
    //         Genus.None => string.Empty,
    //         _ => throw new NotImplementedException($"Cannot match what suffix to give {name}")
    //     };

    //     return suffix;
    // }

    public static bool IsMatch(IDenominational denom, string inputName)
    {
        List<INoun> names = [denom.Name, ..denom.Synonyms?? [] ];
        
        return names.Any(n => n.IsMatch(inputName));
    }
}