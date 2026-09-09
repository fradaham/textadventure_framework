using Nai.TextAdventure.Concepts;

namespace Nai.TextAdventure.Language;

// public class Command
// {
//     public string? ErrorMessage { get; }

//     public ErrorType ErrorType { get; }

//     public Predicate? Predicate { get; }

//     public IDenominational? DirectObject { get; }

//     public string? ObjectSeparator { get; }

//     public IDenominational? IndirectObject { get; }

//     public Command(Predicate predicate)
//     {
//         Predicate = predicate;
//     }
//     public Command(Predicate predicate, IDenominational directObj): this(predicate)
//     {
//         DirectObject = directObj;
//     }
//     public Command(Predicate predicate, IDenominational directObj, IDenominational indirectObj, string objectSeparator): this(predicate, directObj)
//     {
//         IndirectObject = indirectObj;
//         ObjectSeparator = objectSeparator;
//     }

//     public Command(ErrorType errorType, string errorMessage)
//     {
//         ErrorType = errorType;
//         ErrorMessage = errorMessage;
//     }

//     public Command(Predicate predicate, ErrorType errorType, string errorMessage): this(errorType,  errorMessage)
//     {
//         Predicate = predicate;
//     }
//     public Command(Predicate predicate, IDenominational directObj, ErrorType errorType, string errorMessage): this(predicate, errorType, errorMessage)
//     {
//         DirectObject = directObj;
//     }
//     public Command(Predicate predicate, IDenominational directObj, IDenominational indirectObj, ErrorType errorType, string errorMessage): this(predicate, directObj, errorType, errorMessage)
//     {
//         IndirectObject = indirectObj;
//     }
// }

// public enum ErrorType
// {
//     General,
//     NoContent,
//     MissingPredicate,
//     UnknownPredicate,
//     MissingDirectObject,
//     MissingIndirectObject,
//     UnknownDirectObject,
//     UnknownIndirectObject,
//     UnknownSeparator
// }

