using PageEngine.Models;
using PageEngine.Templating;

namespace PageEngine.Generators;

public sealed class TagPageGenerator(TemplateEngine templateEngine)
{
    public string Generate(string tag, IReadOnlyList<Post> posts, SiteConfig config)
    {
        throw new NotImplementedException();
    }
}