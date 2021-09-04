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
            CreateMap<EntityQuery, ByEntityFilter>();

            CreateMap<TopicCreateRequest, TopicCreate>();
            CreateMap<TopicUpdateRequest, TopicUpdate>();

            CreateMap<ParagraphCreateRequest, ParagraphCreate>();
            CreateMap<ParagraphUpdateRequest, ParagraphUpdate>();

            CreateMap<MainEntityQuery, ByMainEntityFilter>();

            CreateMap<NoteCreateRequest, NoteCreate>();
            CreateMap<NoteUpdateRequest, NoteUpdate>();

            CreateMap<FileMessageUpdateRequest, FileMessageUpdate>()
                .ForMember(dest => dest.Explanation, opt => opt.MapFrom(src => src.Message));

        }
    }
}
