namespace e_commerce_server.src.Core.Config
{
    public class FileConfig
    {
        public static string ApplyUploadFilesPath(string? fileName = null)
        {
            return string.IsNullOrEmpty(fileName) ? 
                Path.Combine(Directory.GetCurrentDirectory(), "src", "Core", "Files", "Uploads", "Media") :
                Path.Combine(Directory.GetCurrentDirectory(), "src", "Core", "Files", "Uploads", "Media", fileName);
        }
        public static string ApplyRequestPath()
        {
            return "/files";
        }
        public static string ApplyFileProviderPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "src", "Core", "Files", "Statics");
        }
        public static List<string> ApplyExtensions()
        {
            return new List<string> { ".jpg", ".png", ".jpeg" };
        }
    }
}
