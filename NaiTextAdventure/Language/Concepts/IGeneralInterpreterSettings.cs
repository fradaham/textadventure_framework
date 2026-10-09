namespace Nai.TextAdventure.Language.Concepts;

public interface IGeneralInterpreterSettings
{   
    IEnumerable<Command> Commands { get; }

    string CommandListHelp();

    string NoMatchingPredicateFound(string input);

    string NoPatternMatchForPredicate(IPredicate predicate, string rest);
    
}