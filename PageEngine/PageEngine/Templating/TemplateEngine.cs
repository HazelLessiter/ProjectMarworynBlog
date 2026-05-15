namespace PageEngine.Templating;

public sealed class TemplateEngine(string templateDirectory)
{
    // Renders an inner template, then wraps it in layout.html via the {{content}} token.
    public string Render(string innerTemplateName, IReadOnlyDictionary<string, string> tokens)
    {
        throw new NotImplementedException();
    }

    // Renders a template without a layout wrapper (e.g. for RSS, standalone pages).
    public string RenderRaw(string templateName, IReadOnlyDictionary<string, string> tokens)
    {
        throw new NotImplementedException();
    }
}