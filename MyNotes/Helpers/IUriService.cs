using MyNotes.Contracts.V1.Request.Queries;
using System;

namespace MyNotes.Helpers
{
    public interface IUriService
    {
        Uri GetTopicUri(string postId);

        Uri GetAllTopicsUri(PaginationQuery pagination = null);
    }
}
