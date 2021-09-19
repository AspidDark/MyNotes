using MyNotes.Domain.Entities.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyNotes.Domain.Entities.Additional
{
    public class Comment : BaseEntity
    {
        public Guid ParagraphId { get; set; }
        public Paragraph Paragraph { get; set; }
        [MaxLength(300)]
        public string Message { get; set; }
    }
}
