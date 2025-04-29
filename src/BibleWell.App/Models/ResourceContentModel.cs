namespace BibleWell.App.Models;

public class ResourceContentModel
{
    public List<ContentItem> Content { get; set; } = [];
}

public class ContentItem
{
    public TiptapContent Tiptap { get; set; } = new();
}

public class TiptapContent
{
    public string Type { get; set; } = "";
    public List<Node> Content { get; set; } = [];
}

public class Node
{
    public string Type { get; set; } = "";
    public Attributes? Attrs { get; set; }
    public List<Node>? Content { get; set; } // Child nodes, e.g., text inside paragraphs
    public string? Text { get; set; }
    public List<Mark>? Marks { get; set; } // Marks like bold, italic, etc.
}

public class Attributes
{
    public string? Dir { get; set; }
    public int? Level { get; set; } // For headings
    public int? Start { get; set; } // For ordered lists
    public List<BibleVerse>? Verses { get; set; } // For bibleReference
    public string? ResourceId { get; set; } // For resourceReference
    public string? ResourceType { get; set; } // For resourceReference
}

public class Mark
{
    public string Type { get; set; } = "";
    public Attributes? Attrs { get; set; }
}

public class BibleVerse
{
    public string StartVerse { get; set; } = "";
    public string EndVerse { get; set; } = "";
}