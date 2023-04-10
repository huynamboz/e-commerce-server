using e_commerce_server.src.Core.Modules.Media.Service;
using e_commerce_server.src.Core.Config;
using e_commerce_server.src.Packages.HttpExceptions;

namespace e_commerce_server.src.Core.Modules.Media.Interceptor
{
    public class MediaInterceptor
    {
        private List<string> _allowedExtensions;
        public MediaInterceptor()
        {
            this._allowedExtensions = FileConfig.ApplyExtensions();
        }
        public void Validate(List<IFormFile> files, int fileQuantity)
        {
            if (files.Count > fileQuantity)
            {
                throw new BadRequestException(MediaEnum.QUANTITY_TOO_MUCH);
            }

            foreach (var file in files)
            {
                if (!this._allowedExtensions.Contains(Path.GetExtension(file.FileName)))
                {
                    throw new BadRequestException(MediaEnum.EXTENSION_NOT_ALLOWED);
                }
            }
        }
    }
}
