using Microsoft.AspNetCore.Http;
using MyNotes.Services.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyNotes.Services.Helpers
{
    public class FileHelper : IFileHelper
    {
        private readonly FilePathOptions _filePathOptions;
        public FileHelper(FilePathOptions filePathOptions)
        {
            _filePathOptions = filePathOptions;
        }

        public FileStream GetFileStream(string path)
        {
            try
            {
                string fileCompletePath = _filePathOptions.BasePath + path;
                var image = File.OpenRead(fileCompletePath);
                return image;
            }
            catch (Exception ex)
            {
                throw;
            }
            //return File(image, "image/jpeg");
        }

        public async Task<bool> SaveFile(IFormFile fromFile, string path)
        {
            try
            {
                string filePath = _filePathOptions.BasePath + path;
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fromFile.CopyToAsync(fileStream);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteFile(string path)
        {
            try
            {
                string filePath = _filePathOptions.BasePath + path;
                File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
