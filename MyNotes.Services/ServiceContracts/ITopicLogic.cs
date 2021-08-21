using MyNotes.Contracts.V1;
using MyNotes.Services.InternalDto;
using System;
using System.Threading.Tasks;

namespace MyNotes.Services.ServiceContracts
{
    public interface ITopicLogic
    {
        Task<BaseResponseDto> Create(TopicCreate topicCreate);
        Task<BaseResponseDto> Delete(Guid topicId, Guid userId);
        Task<BaseResponseDto> Get(EntityByUserIdFilter entityByUserIdFilter);
        Task<BaseResponseDto> GetList(BaseUserIdFilter baseUserIdFilter, PaginationFilter paginationFilter);
        Task<BaseResponseDto> Update(TopicUpdate topicUpdate);
    }
}