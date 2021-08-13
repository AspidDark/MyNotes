using System;

namespace MyNotes.Domain.Entities.Core
{
    public class FileEntity : BaseNoteEntity
    {
        public Guid ParagraphId { get; set; }
        public Paragraph Paragraph { get; set; }
        public string Path { get; set; }
    }
}
