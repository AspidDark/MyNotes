using MyNotes.Domain.Enums;
using System;

namespace MyNotes.Domain.Entities.Rights
{
    public class LocalRight : BaseNoteEntity
    {
        public Guid AllowedToUserId { get; set; }

        public AccessType AccessType { get; set; }

        public EntityType EntityType { get; set; }

        public Guid EntityId { get; set; }
    }
}
