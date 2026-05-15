namespace PageEngine.Models;

public record Post(string Title,
    DateOnly Date,
    string Category,
    string CategorySlug,
    IReadOnlyList<string> Tags,
    string Slug,
    string Content,
    string Excerpt,
    string SourcePath);