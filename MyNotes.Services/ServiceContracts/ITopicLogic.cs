using MyNotes.Contracts.V1;
using MyNotes.Services.InternalDto;
using System;
using System.Threading.Tasks;

namespace MyNotes.Services.ServiceContracts
{
    public interface ITopicLogic
    {
        Task<BaseResponse> Create(TopicCreate topicCreate);
        Task<BaseResponse> Delete(Guid topicId, Guid userId);
        Task<BaseResponse> Get(ByEntityFilter entityByUserIdFilter);
        Task<BaseResponse> GetList(BaseUserIdFilter baseUserIdFilter, PaginationFilter paginationFilter);
        Task<BaseResponse> Update(TopicUpdate topicUpdate);
    }
}