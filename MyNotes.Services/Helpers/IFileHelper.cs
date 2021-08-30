using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace MyNotes.Services.Helpers
{
    public interface IFileHelper
    {
        FileStream GetFileStream(string path);

        Task<bool> SaveFile(IFormFile fromFile, string path);

        Task<bool> DeleteFile(string path);
    }
}