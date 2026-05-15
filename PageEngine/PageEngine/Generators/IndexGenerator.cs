using PageEngine.Models;
using PageEngine.Templating;

namespace PageEngine.Generators;

public sealed class IndexGenerator(TemplateEngine templateEngine)
{
    public string GenerateSiteIndex(IReadOnlyList<Post> allPosts, SiteConfig config)
    {
        throw new NotImplementedException();
    }

    public string GenerateCategoryIndex(string categoryDisplayName, IReadOnlyList<Post> posts, SiteConfig config)
    {
        throw new NotImplementedException();
    }
}