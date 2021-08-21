using System;

namespace MyNotes.Contracts.V1.Response
{
    public class BaseResponsseDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime EditDate { get; set; }
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid OwnerId { get; set; }
    }
}
