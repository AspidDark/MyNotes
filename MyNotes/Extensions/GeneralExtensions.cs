using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace MyNotes.Extensions
{
    public static class GeneralExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User is not null)
            {
                var userId= httpContext.User.Claims.Single(x => x.Type == "id").Value;
                if (Guid.TryParse(userId, out Guid id))
                {
                    return id;
                }
            }
            return Guid.Empty;
        }
    }
}
