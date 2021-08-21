using Microsoft.AspNetCore.Mvc;
using System;

namespace MyNotes.Contracts.V1.Request.Queries
{
    public class EntityByUserIdQuery : BaseUserIdQuery
    {
        [FromQuery(Name = "entityId")]
        public Guid EntityId { get; set; }
    }
}
