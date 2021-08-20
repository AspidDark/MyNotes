using Microsoft.AspNetCore.Mvc;

namespace MyNotes.Contracts.V1.Request.Queries
{
    public class BaseUserIdQuery
    {
        [FromQuery(Name = "userId")]
        public string UserId { get; set; }
    }
}
