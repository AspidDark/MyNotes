using System;

namespace MyNotes.Services.InternalDto
{
    public class EntityByUserIdFilter : BaseUserIdFilter
    {
        public Guid EntityId { get; set; }
    }
}
