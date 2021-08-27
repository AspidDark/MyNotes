using Microsoft.AspNetCore.Mvc;
using System;

namespace MyNotes.Contracts.V1.Request.Queries
{
    public class MainEntityQuery
    {
        [FromQuery(Name = "entityId")]
        public Guid EntityId { get; set; }

        [FromQuery(Name = "mainEntityId")]
        public Guid MainEntityId { get; set; }
    }
}
