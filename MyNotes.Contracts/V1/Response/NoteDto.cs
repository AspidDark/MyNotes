using System;

namespace MyNotes.Contracts.V1.Response
{
    public class NoteDto : BaseResponseDto
    {
        public string Message { get; set; }

        public Guid ParagraphId { get; set; }
    }
}
