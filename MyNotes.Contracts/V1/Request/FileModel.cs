using Microsoft.AspNetCore.Http;
using System;

namespace MyNotes.Contracts.V1.Request
{
    public class FileModel
    {
        public IFormFile FormFile { get; set; }

        public Guid ParagraphId { get; set; }
    }
}
