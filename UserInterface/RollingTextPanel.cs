using Spectre.Console;
using Spectre.Console.Rendering;

namespace TextAdventure.UserInterface;

public class RollingTextPanel
{
    public int Width 
    { 
        get
        {
            return _width;
        }
        set
        {
            _width = value;
            UpdatePanel();
        } 
    }

    private int _width;

    public int Height
    {
        get
        {
            return _height;
        }
        set
        {
            _height = value;
            UpdatePanel();
        }
    }
    private int _height;
    public string Text { get; set; }
    public string Header { get; set; }
    public Panel InnerPanel { get; internal set; }

    private int currentLine;
    private List<string> allLines;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public RollingTextPanel(string text, string header, int width, int height)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
       Text = text;
       Header = header;
       Height = height;
       Width = width;
       currentLine = 0;
       UpdatePanel();
    }

    public void ScrollUp()
    {
        if (currentLine > 0)
        {
            currentLine--;
            UpdatePanel();
        }
    }

    public void ScrollDown()
    {
        if (currentLine < allLines.Count - Height)
        {
            currentLine++;
            UpdatePanel();
        }
    }

    public void Append(string text)
    {
        Text += text; 
        UpdatePanel();
        if (currentLine < allLines.Count - Height)
        {
            currentLine = allLines.Count - Height;   
        }
    }

    public void Append(string text, string style)
    {
        Text += $"[style]{text}[/]"; 
        UpdatePanel();       
        if (currentLine < allLines.Count - Height)
        {
            currentLine = allLines.Count - Height;   
        }
    }

    private void UpdatePanel()
    {
        allLines = GetWrappedLines();
        int lastLine = currentLine + Height >= allLines.Count()? allLines.Count() - 1 : currentLine + Height - 1;
        IEnumerable<string> visibleLines = allLines[currentLine..(lastLine + 1)]; 
        Markup storyMarkup = new Markup(string.Join("\n", visibleLines)).Overflow(Overflow.Crop);
        InnerPanel = new Panel(storyMarkup);//new Panel(new Padder(storyMarkup, new Padding(1,1)));
        InnerPanel.Expand().BorderColor(Color.Green).Header(Header);
    }

    private List<string> GetWrappedLines()
    {
        string[] lines = Text.Split("\n", StringSplitOptions.None);
        List<string> foldedLines = new();

        foreach(string line in lines)
        {
            string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (words.Count() == 0)
            {
                foldedLines.Add(String.Empty);
            }
            else
            {
                string foldedLine = String.Empty;
                for (int i = 0; i < words.Length; i++)
                {
                    string word = words[i];
                    if (foldedLine.Length + word.Length < Width)
                    {
                        foldedLine += $"{word} ";
                    }
                    else
                    {
                        foldedLines.Add(foldedLine.TrimEnd());
                        foldedLine = $"{word} ";
                    }
                    if (i == words.Length - 1)
                    {
                        foldedLines.Add(foldedLine.TrimEnd());
                    }
                }
            }
        }
        return foldedLines;
    }
}