using System;

namespace MyNotes.Domain.Entities.Core
{
    public class FileEntity : BaseNoteEntity
    {
        public Guid ParagraphId { get; set; }
        public Paragraph Paragraph { get; set; }
        public string  Explanation { get; set; }
        /// <summary>
        /// Name of file given by user
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// File on Disc name
        /// </summary>
        public string SavedFileName { get; set; }
    }
}
