using System;

namespace MyNotes.Contracts.V1.Response
{
    public class FileMessageResponseDto
    {
        public Guid ParagraphId { get; set; }

        public Guid Id { get; set; }

        public string Message { get; set; }
    }
}
