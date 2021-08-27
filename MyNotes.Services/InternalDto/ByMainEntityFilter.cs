using System;

namespace MyNotes.Services.InternalDto
{
    public class ByMainEntityFilter : BaseUserIdFilter
    {
        public Guid EntityId { get; set; }

        public Guid MainEntityId { get; set; }
    }
}
