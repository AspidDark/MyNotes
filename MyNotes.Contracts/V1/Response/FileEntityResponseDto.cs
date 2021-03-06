using System;
using System.IO;

namespace MyNotes.Contracts.V1.Response
{
    public class FileEntityResponseDto
    {
        public FileStream FileEntity { get; set; }

        public string FileName { get; set; }

        public Guid ParagraphId { get; set; }

        public Guid Id { get; set; }
    }
}
