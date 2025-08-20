using System.Reflection;

namespace Ecommerce.Infrastructure.Helpers
{
    public static class FilesHelpers
    {
        public static string ReadEmbeddedResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fullResourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(name => name.EndsWith(resourceName));

            if (fullResourceName == null)
                throw new FileNotFoundException($"Resource '{resourceName}' não encontrado");

            using var stream = assembly.GetManifestResourceStream(fullResourceName);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
