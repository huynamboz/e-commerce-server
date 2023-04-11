using e_commerce_server.src.Core.Config;
using e_commerce_server.src.Core.Modules.Media.Interceptor;

namespace e_commerce_server.src.Core.Modules.Media
{
    public class MediaHandler
    {
        private MediaInterceptor mediaInterceptor;
        private List<IFormFile> files;
        public MediaHandler()
        {
            mediaInterceptor = new MediaInterceptor();
            files = new List<IFormFile>();
        }
        public MediaHandler Validate(List<IFormFile> files, int quantity = 10)
        {
            this.files = files;
            mediaInterceptor.Validate(files, quantity);
            return this;
        }
        public async Task<List<string>> Save()
        {
            List<string> filePaths = new List<string>();

            foreach (var file in files)
            {
                var fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{Path.GetExtension(file.FileName)}";

                var filePath = FileConfig.ApplyUploadFilesPath(fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                filePaths.Add(filePath);
            }
            return filePaths;
        }
    }
}
