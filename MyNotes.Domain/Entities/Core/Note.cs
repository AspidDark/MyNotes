using System;
using System.ComponentModel.DataAnnotations;

namespace MyNotes.Domain.Entities.Core
{
    public class Note : BaseNoteEntity
    {
        [MaxLength(500)]
        public string Message { get; set; }

        public Guid ParagraphId { get; set; }

        public Paragraph Paragraph { get; set; }

    }
}
