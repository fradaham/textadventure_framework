using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.General;

public interface IGeneralInterpreterSettings
{   
    IEnumerable<Command> Commands { get; }

    GameLanguage Language { get; }

    string CommandListHelp();

    string CommandHelp(Command command);

    string NoMatchingPredicateFound(string input);

    string NoPatternMatchForPredicate(IPredicate predicate, string rest);
    
}