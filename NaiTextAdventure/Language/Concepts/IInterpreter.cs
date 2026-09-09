using System.Text.RegularExpressions;

namespace Nai.TextAdventure.Language.Concepts
{
    public interface IInterpreter
    {
        public ParsingResult Parse(string input);
    }
}

