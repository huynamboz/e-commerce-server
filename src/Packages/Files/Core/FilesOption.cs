using e_commerce_server.src.Core.Config;
using Microsoft.Extensions.FileProviders;

namespace e_commerce_server.src.Packages.Files.Core
{
    public class FilesOption
    {
        private readonly string _fileProviderPath;
        private readonly string _requestPath;
        private FilesOption() {
            this._fileProviderPath = FileConfig.ApplyFileProviderPath();
            this._requestPath = FileConfig.ApplyRequestPath();
        }
        public static FilesOption Builder() {
            return new FilesOption();
        }
        public StaticFileOptions ConfigureStaticFilesOptions() {
            return new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(this._fileProviderPath),
                RequestPath = new PathString(this._requestPath)
            };
        }
    }
}
