using System;

namespace MyNotes.Contracts.V1.Request
{
    public class FileMessageUpdateRequest
    {
        public string Message { get; set; }

        public Guid ParagraphId { get; set; }

        public Guid FileId { get; set; }
    }
}
