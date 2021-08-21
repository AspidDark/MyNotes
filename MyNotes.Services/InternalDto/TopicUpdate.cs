using System;

namespace MyNotes.Services.InternalDto
{
    public class TopicUpdate
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public Guid TopicId { get; set; }
    }
}
