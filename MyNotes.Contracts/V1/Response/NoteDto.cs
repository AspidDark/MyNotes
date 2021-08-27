using System;

namespace MyNotes.Contracts.V1.Response
{
    public class NoteDto
    {
        public string Message { get; set; }

        public Guid ParagraphId { get; set; }

        public Guid Id { get; set; }
    }
}
