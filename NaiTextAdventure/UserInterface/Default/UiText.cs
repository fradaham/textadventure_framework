using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.UserInterface.Default;

public static class UiText
{
    private static string LanguageSelector(GameLanguage language, string swedish, string english) =>
        language == GameLanguage.English ? english : swedish;

    public static string StartGame(GameLanguage language) =>
        LanguageSelector(language, "Starta spelet", "Start game");

    public static string AboutOption(GameLanguage language) =>
        LanguageSelector(language, "Om", "About");

    public static string QuitOption(GameLanguage language) =>
        LanguageSelector(language, "Avsluta", "Quit");

    public static string RestartGame(GameLanguage language) =>
        LanguageSelector(language, "Starta om spelet", "Restart game");

    public static string PresentedBy(GameLanguage language, string creator) =>
        LanguageSelector(language, $"{creator} presenterar", $"{creator} presents");

    public static string AboutTitle(GameLanguage language) =>
        LanguageSelector(language, "OM SPELET", "ABOUT");

    public static string BackToTitle(GameLanguage language) =>
        LanguageSelector(language, "Tillbaka till titelskärmen", "Back to title screen");

    public static string PressAnyKeyToContinue(GameLanguage language) =>
        LanguageSelector(language, "Tryck valfri tangent för att fortsätta.", "Press any key to continue.");

    public static string YouDied(GameLanguage language) =>
        LanguageSelector(language, "DU DOG!", "YOU DIED!");

    public static string YouCompletedTheGame(GameLanguage language) =>
        LanguageSelector(language, "DU KLARADE SPELET!", "YOU COMPLETED THE GAME!");

    public static string PlayerStatus(GameLanguage language) =>
        LanguageSelector(language, "SPELARSTATUS", "PLAYER STATUS");

    public static string Health(GameLanguage language) =>
        LanguageSelector(language, "Hälsa", "Health");

    public static string FightingSkill(GameLanguage language) =>
        LanguageSelector(language, "Stridsförmåga", "Combat skill");

    public static string Location(GameLanguage language) =>
        LanguageSelector(language, "Plats", "Location");

    public static string Enemy(GameLanguage language) =>
        LanguageSelector(language, "Fiende", "Enemy");

    public static string Player(GameLanguage language) =>
        LanguageSelector(language, "Spelare", "Player");

    public static string Logbook(GameLanguage language) =>
        LanguageSelector(language, "LOGGBOK", "LOG");

    public static string Inventory(GameLanguage language) =>
        LanguageSelector(language, "PACKNING", "INVENTORY");

    public static string FightTitle(GameLanguage language) =>
        LanguageSelector(language, "STRID!", "FIGHT!");

    public static string PressAnyKeyToStart(GameLanguage language) =>
        LanguageSelector(language, "Tryck på valfri tangent för att börja", "Press any key to start");

    public static string PressAnyKeyForRound(GameLanguage language, int round) =>
        LanguageSelector(language, $"Tryck på valfri tangent för rond {round}.", $"Press any key for round {round}.");

    public static string EnemyTookDamage(GameLanguage language, int damage) =>
        LanguageSelector(language, $"Fienden tog {damage} i skada. Hähä!", $"The enemy took {damage} damage. Hah!");

    public static string PlayerTookDamage(GameLanguage language, string playerName, int damage) =>
        LanguageSelector(language, $"{playerName} tog {damage} i skada. Urk!", $"{playerName} took {damage} damage. Ugh!");

    public static string EvenRound(GameLanguage language) =>
        LanguageSelector(language, "Jämnstark rond. Ingen tog skada.", "Even round. No one took damage.");

    public static string Defeat(GameLanguage language) =>
        LanguageSelector(language, "Nederlag!", "Defeat!");

    public static string Victory(GameLanguage language) =>
        LanguageSelector(language, "Seger!", "Victory!");

    public static string PressAnyKeyToBeginFight(GameLanguage language, string enemyName) =>
        LanguageSelector(language, $"Tryck valfri tangent för för att börja striden mot {enemyName}.", $"Press any key to start fighting {enemyName}.");

    public static string ScrollUp(GameLanguage language) =>
        LanguageSelector(language, "text uppåt", "scroll up");
    
    public static string ScrollDown(GameLanguage language) =>
        LanguageSelector(language, "text nedåt", "scroll down");
}
