using TextAdventure.Language;

namespace TextAdventure.Concepts;

public interface IEntity: IInteractable, IDenominational
{
    Guid Id {get;}

    string Description { get; }
}

// public interface IStatefullItem<T>: IItem
// {
//      T State {get; set;}
// }

// public interface State<T>
// {
//     T State { get; }

//     string Name { get; }
// }




