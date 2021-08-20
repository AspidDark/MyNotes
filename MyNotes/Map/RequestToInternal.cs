using AutoMapper;
using MyNotes.Contracts.V1.Request.Queries;
using MyNotes.Services.InternalDto;

namespace MyNotes.Map
{
    public class RequestToInternal : Profile
    {
        public RequestToInternal()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<BaseUserIdQuery, BaseUserIdFilter>();
            CreateMap<EntityByUserIdQuery, EntityByUserIdFilter>();
        }
    }
}
