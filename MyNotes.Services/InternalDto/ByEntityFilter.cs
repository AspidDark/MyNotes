using System;

namespace MyNotes.Services.InternalDto
{
    public class ByEntityFilter : BaseUserIdFilter
    {
        public Guid EntityId { get; set; }
    }
}
