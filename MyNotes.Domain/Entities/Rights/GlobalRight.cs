using MyNotes.Domain.Enums;
using System;

namespace MyNotes.Domain.Entities.Rights
{
    public class GlobalRight : BaseNoteEntity
    {
        public Guid UserId { get; set; }

        public AccessType AccessType { get; set; }
    }
}
