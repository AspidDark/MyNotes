using AutoMapper;
using MyNotes.Contracts.V1.Response;
using MyNotes.Domain.Entities.Core;

namespace MyNotes.Services.Map
{
    public class EntityToResponse : Profile
    {
        public EntityToResponse()
        {
            CreateMap<Topic, TopicDto>();

            CreateMap<Paragraph, ParagraphDto>();
        }
    }
}
