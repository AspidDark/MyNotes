using System.ComponentModel.DataAnnotations;

namespace MyNotes.Domain.Entities.Core
{
    public class Topic : BaseEntity
    {
        [MaxLength(150)]
        public string Name { get; set; }
    }
}
