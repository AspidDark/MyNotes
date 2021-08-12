using System;

namespace MyNotes.Domain.Entities.Core
{
    public class FileEntity : BaseNoteEntity
    {
        public Guid NoteId { get; set; }
        public Note Note { get; set; }
        public string Path { get; set; }
    }
}
