using NAudio.Sequencing;

namespace Nai.TextAdventure.Concepts;

//Below a try to create different types of events instead of one general type (ActionResult). Not used yet.
public record ActionEvent(string Message);

public record BattleEvent(string Message, Opponent Fight): ActionEvent(Message);

public record MovementEvent(string Message, string TargetRoom): ActionEvent(Message);

public record GameEvent(string Message, GameResult GameResult): ActionEvent(Message);

public class ActionResult
{
    public required string Message { get; init;}

    public string? MoveToRoomId { get; init;}

    public int? DeltaHealth { get; init;}

    public Action<Context>? Custom {get; init;}

    public Opponent? Fight { get; init;} 

    public GameResult? GameResult { get; init; }

}

public enum GameResult
{
    Success,
    Fail
}

public class Opponent
{
    public required string Name {get; set;}

    public required int FightingSkill {get; set;}

    public required int Health {get; set;}

    public ActionResult? SuccessEvent {get; set;}

    public ActionResult? FailEvent {get; set;}
}

//TODO: Structural help for story events that happen only once, and can have dependencies

// public enum StoryEventType
// {
//     Success,
//     Fail,
//     Fight,
//     Story
// }

// public class StoryEvent
// {
//     public required string Id {get; init;}
//     public required StoryEventType Type {get; init;} = StoryEventType.Story;

//     public required string Message {get; init;}

//     public string? Fight { get; init;} //This should be a complex type


// }