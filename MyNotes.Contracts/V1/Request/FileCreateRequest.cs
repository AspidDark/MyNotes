using Microsoft.AspNetCore.Http;
using System;

namespace MyNotes.Contracts.V1.Request
{
    public class FileCreateRequest
    {
        public IFormFile FileBody { get; set; }

        public Guid ParagraphId { get; set; }
    }
}
