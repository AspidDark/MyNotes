using System;
using System.ComponentModel.DataAnnotations;

namespace MyNotes.Domain.Entities.Core
{
    public class Note : BaseEntity
    {
        [MaxLength(1500)]
        public string Message { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }

        public Guid ParagraphId { get; set; }

        public Paragraph Paragraph { get; set; }

    }
}
