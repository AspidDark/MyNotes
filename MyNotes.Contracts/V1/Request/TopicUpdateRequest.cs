using System;

namespace MyNotes.Contracts.V1.Request
{
    public class TopicUpdateRequest
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; set; }

        public string Name { get; set; }
    }
}
