namespace PageEngine.IO;

public sealed class OutputWriter(string outputDirectory)
{
    public void Write(string relativePath, string content)
    {
        throw new NotImplementedException();
    }

    // Copies static assets (css/, images/) from source to output directory.
    public void CopyAssets(string sourceDirectory)
    {
        throw new NotImplementedException();
    }
}