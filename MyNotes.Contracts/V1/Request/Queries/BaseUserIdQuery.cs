using Microsoft.AspNetCore.Mvc;
using System;

namespace MyNotes.Contracts.V1.Request.Queries
{
    public class BaseUserIdQuery
    {
        [FromQuery(Name = "userId")]
        public Guid UserId { get; set; }
    }
}
