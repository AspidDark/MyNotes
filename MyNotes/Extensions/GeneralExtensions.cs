using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace MyNotes.Extensions
{
    public static class GeneralExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User is not null)
            {
                var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                if (Guid.TryParse(userId, out Guid id))
                {
                    return id;
                }
            }
            return Guid.Empty;
            // "c2f8bbf7-0564-4ad6-9a4d-4925a037e153"
            //return Guid.Parse("c2f8bbf7-0564-4ad6-9a4d-4925a037e152");
        }

        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            string userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Guid.Parse(userId);
        }


    }
}
