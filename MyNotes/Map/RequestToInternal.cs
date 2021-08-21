using AutoMapper;
using MyNotes.Contracts.V1.Request;
using MyNotes.Contracts.V1.Request.Queries;
using MyNotes.Services.InternalDto;

namespace MyNotes.Map
{
    public class RequestToInternal : Profile
    {
        public RequestToInternal()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<EntityQuery, EntityByUserIdFilter>();

            CreateMap<TopicCreateRequest, TopicCreate>();
            CreateMap<TopicUpdateRequest, TopicUpdate>();

        }
    }
}
