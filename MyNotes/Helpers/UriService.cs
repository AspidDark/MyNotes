using Microsoft.AspNetCore.WebUtilities;
using MyNotes.Contracts.V1;
using MyNotes.Contracts.V1.Request.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Helpers
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetTopicUri(string postId)
        {
            return new Uri(_baseUri + ApiRoutes.Topics.Get.Replace("{postId}", postId));
        }

        public Uri GetAllTopicsUri(PaginationQuery pagination = null)
        {
            var uri = new Uri(_baseUri);

            if (pagination == null)
            {
                return uri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", pagination.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());

            return new Uri(modifiedUri);
        }
    }
}
