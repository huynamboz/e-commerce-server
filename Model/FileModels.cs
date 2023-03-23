using System.ComponentModel.DataAnnotations;

namespace e_commerce_server.Model
{
    public class FileModels
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
    }
}
