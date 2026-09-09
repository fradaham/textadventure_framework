namespace Nai.TextAdventure.Concepts;

public class ActionResult
{
    public required string Message { get; init;}

    public string? MoveToRoomId { get; init;}

    public int? DeltaHealth { get; init;}

    public StoryEvent? StoryEvent { get; init; }

}

public enum StoryEvent
{
    Success,
    Fail,
    Fight
}