using System.ComponentModel.DataAnnotations;

namespace MyNotes.Domain.Entities.Helping
{
    public class Tag : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
