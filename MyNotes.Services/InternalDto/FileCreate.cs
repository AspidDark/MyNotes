using Microsoft.AspNetCore.Http;
using System;

namespace MyNotes.Services.InternalDto
{
    public class FileCreate
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; set; }

        public IFormFile FileBody { get; set; }

        public Guid ParagraphId { get; set; }

    }
}
