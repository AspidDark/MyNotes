using System;

namespace MyNotes.Services.InternalDto
{
    public class ParagraphUpdate
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public Guid ParagraphId { get; set; }
    }
}
