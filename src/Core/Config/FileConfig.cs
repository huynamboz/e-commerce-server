namespace e_commerce_server.src.Core.Config
{
    public class FileConfig
    {
        public static string ApplyUploadFilesPath(string? fileName = null)
        {
            return string.IsNullOrEmpty(fileName) ? 
                Path.Combine(Directory.GetCurrentDirectory(), "src", "Core", "Uploads", "Media") :
                Path.Combine(Directory.GetCurrentDirectory(), "src", "Core", "Uploads", "Media", fileName);
        }
        public static string ApplyRequestPath()
        {
            return "/files";
        }
        public static string ApplyFileProviderPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "src", "Core", "Uploads", "Static");
        }
        public static List<string> ApplyExtensions()
        {
            return new List<string> { ".jpg", ".png", ".jpeg" };
        }
    }
}
