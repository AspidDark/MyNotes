using System;

namespace MyNotes.Services.InternalDto
{
    public class TopicCreate
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; set; }

        public string Name { get; set; }
    }
}
