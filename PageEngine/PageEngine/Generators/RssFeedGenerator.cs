using PageEngine.Models;

namespace PageEngine.Generators;

public sealed class RssFeedGenerator
{
    // Returns RSS 2.0 XML string. Uses System.Xml.Linq - no external deps.
    public string Generate(IReadOnlyList<Post> posts, SiteConfig config)
    {
        throw new NotImplementedException();
    }
}