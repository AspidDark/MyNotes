using MyNotes.Domain.Entities.Helping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyNotes.Domain.Entities.Core
{
    public class Paragraph : BaseNoteEntity
    {
        [MaxLength(300)]
        public string Name { get; set; }

        public Guid TopicId { get; set; }
        /// <summary>
        /// Тема
        /// </summary>
        public Topic Topic { get; set; }

        ///// <summary>
        ///// Теги
        ///// </summary>
        //public ICollection<Tag> Tags { get; set; }
    }
}
