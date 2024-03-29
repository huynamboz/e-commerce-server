﻿using e_commerce_server.src.Packages.HttpExceptions;

namespace e_commerce_server.src.Core.Modules.Media.Service
{
    public class FileSystemService
    {
        public void DeleteFile(string file)
        {
            try
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            } catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        public void DeleteFiles(List<string> files)
        {
            foreach (string file in files)
            {
                DeleteFile(file);
            }
        }
    }
}
