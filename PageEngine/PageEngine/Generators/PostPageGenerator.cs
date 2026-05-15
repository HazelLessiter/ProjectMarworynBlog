using PageEngine.Models;
using PageEngine.Templating;

namespace PageEngine.Generators;

public sealed class PostPageGenerator(TemplateEngine templateEngine)
{
    public string Generate(Post post, SiteConfig config)
    {
        throw new NotImplementedException();
    }
}