using Microsoft.AspNetCore.Mvc;

namespace MyNotes.Contracts.V1.Request.Queries
{
    public class EntityByUserIdQuery : BaseUserIdQuery
    {
        [FromQuery(Name = "entityId")]
        public string EntityId { get; set; }
    }
}
