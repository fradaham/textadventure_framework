using System.Data.SqlTypes;
using System.Text;
using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.General;

public static class Utils
{
    public static bool IsMatch(IDenominational denom, string inputName)
    {
        List<INoun> names = [denom.Name, ..denom.Synonyms?? [] ];
        
        return names.Any(n => n.IsMatch(inputName));
    }
}