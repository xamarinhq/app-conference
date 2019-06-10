using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace DataManager.Helpers
{
    public static class ResourceHelper
    {
        public static Task<string> GetResourceString(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEndAsync();
            }
        }
    }
}