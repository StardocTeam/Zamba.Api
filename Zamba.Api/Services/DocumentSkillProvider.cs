using System.Reflection;

namespace Zamba.Api.Services;

public interface IDocumentSkillProvider
{
    string? GetInstructions(int? documentTypeCode);
}

public sealed class DocumentSkillProvider : IDocumentSkillProvider
{
    private readonly Assembly _assembly = typeof(DocumentSkillProvider).Assembly;

    public string? GetInstructions(int? documentTypeCode)
    {
        if (documentTypeCode is null)
        {
            return null;
        }

        var resourceSuffix = $".DocumentSkills.{documentTypeCode.Value}.md";
        var resourceName = _assembly.GetManifestResourceNames()
            .SingleOrDefault(name => name.EndsWith(resourceSuffix, StringComparison.OrdinalIgnoreCase));
        if (resourceName is null)
        {
            return null;
        }

        using var resource = _assembly.GetManifestResourceStream(resourceName);
        if (resource is null)
        {
            return null;
        }

        using var reader = new StreamReader(resource);
        return reader.ReadToEnd();
    }
}
