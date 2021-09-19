using MyNotes.Domain.Enums;
using System;

namespace MyNotes.Domain.Entities.Rights
{
    public class GlobalRight : BaseEntity
    {
        public Guid UserId { get; set; }

        public AccessType AccessType { get; set; }
    }
}
