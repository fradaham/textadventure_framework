using System.Text.RegularExpressions;

namespace TextAdventure.Language.Concepts
{
    public interface IInterpreter
    {
        public ParsingResult Parse(string input);
    }
}

