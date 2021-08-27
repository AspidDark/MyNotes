using System;

namespace MyNotes.Services.InternalDto
{
    public class NoteUpdate
    {
        public string Message { get; set; }

        public Guid UserId { get; set; }

        public Guid NoteId { get; set; }
    }
}
