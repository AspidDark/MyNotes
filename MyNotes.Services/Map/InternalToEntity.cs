using AutoMapper;
using MyNotes.Domain.Entities.Core;
using MyNotes.Services.InternalDto;

namespace MyNotes.Services.Map
{
    public class InternalToEntity : Profile
    {
        public InternalToEntity()
        {
            CreateMap<TopicCreate,Topic>().ForMember(dest=>dest.OwnerId, opt=>opt.MapFrom(src=>src.UserId));
            CreateMap<TopicUpdate,Topic>().ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
