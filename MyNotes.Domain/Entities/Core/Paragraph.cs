using System;
using System.ComponentModel.DataAnnotations;

namespace MyNotes.Domain.Entities.Core
{
    public class Paragraph : BaseEntity
    {
        [MaxLength(300)]
        public string Name { get; set; }

        public Guid TopicId { get; set; }
        /// <summary>
        /// Тема
        /// </summary>
        public Topic Topic { get; set; }

        /// <summary>
        /// Текст параграфа
        /// </summary>
        [MaxLength(5000)]
        public string Message { get; set; }

        ///// <summary>
        ///// Теги
        ///// </summary>
        //public ICollection<Tag> Tags { get; set; }
    }
}
