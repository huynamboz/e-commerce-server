namespace e_commerce_server.src.Core.Config
{
    public class FileConfig
    {
        public static string ApplyFileProviderPath(string? fileName = null)
        {
            return string.IsNullOrEmpty(fileName) ? 
                Path.Combine(Directory.GetCurrentDirectory(), "src", "Core", "Uploads", "Media") :
                Path.Combine(Directory.GetCurrentDirectory(), "src", "Core", "Uploads", "Media", fileName);
        }
        public static string ApplyRequestPath()
        {
            return "/media";
        }
        public static List<string> ApplyExtensions() {
            return new List<string> { ".jpg", ".png", ".jpeg" };
        }
    }
}
