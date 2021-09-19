using System;

namespace MyNotes.Domain.Entities
{
    public class BaseEntity
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

        /// <summary>
        /// Удалено? 0 нет 1 да
        /// </summary>
        public int IsDeleted { get; set; }

    }
}
