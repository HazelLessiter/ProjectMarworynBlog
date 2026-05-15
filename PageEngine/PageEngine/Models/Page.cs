namespace PageEngine.Models;

public record Page(string Title,
    string Slug,
    string Content,
    string SourcePath);