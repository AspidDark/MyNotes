using System;

namespace MyNotes.Services.InternalDto
{
    public class FileMessageUpdate
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; set; }

        public string Explanation { get; set; }

        public Guid ParagraphId { get; set; }

        public Guid FileId { get; set; }
    }
}
