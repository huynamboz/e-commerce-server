﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using e_commerce_server.Src.Core.Env;
using e_commerce_server.Src.Packages.HttpException;

namespace e_commerce_server.src.Core.Modules.Media.Service
{
    public class MediaService
    {
        private FileSystemService fileSystemService;
        private Cloudinary cloudinary;

        public MediaService()
        {
            fileSystemService = new FileSystemService();
            cloudinary = new Cloudinary(new Account(
               ENV.CLOUDINARY_NAME,
               ENV.CLOUDINARY_API_KEY,
               ENV.CLOUDINARY_API_SECRET
            ));
        }
        public string UploadOne(string filePath, string? folderPath = null, string? publicId = null) 
        {
            try
            {
                var response = cloudinary.Upload(new ImageUploadParams()
                {
                    File = new FileDescription(filePath),
                    PublicId = publicId,
                    Folder = folderPath,
                    Overwrite = !string.IsNullOrEmpty(publicId)
                });

                return response.SecureUrl.ToString();
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            } finally
            {
                fileSystemService.DeleteFile(filePath);
            }
        }
    }
}