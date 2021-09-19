using MyNotes.Domain.Enums;
using System;

namespace MyNotes.Domain.Entities.Rights
{
    public class LocalRight : BaseEntity
    {
        public Guid AllowedUserId { get; set; }

        public AccessType AccessType { get; set; }

        public EntityType EntityType { get; set; }

        public Guid EntityId { get; set; }
    }
}
