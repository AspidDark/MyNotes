using System;

namespace MyNotes.Contracts.V1.Request
{
    public class NoteCreateRequest
    {
        public string Message { get; set; }

        public string Name { get; set; }

        public Guid ParagraphId { get; set; }
    }
}
