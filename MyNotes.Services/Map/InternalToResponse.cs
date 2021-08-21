using AutoMapper;
using MyNotes.Contracts.V1.Response;
using MyNotes.Domain.Entities.Core;

namespace MyNotes.Services.Map
{
    public class InternalToResponse : Profile
    {
        public InternalToResponse()
        {
            CreateMap<Topic, TopicDto>();
        }
    }
}
