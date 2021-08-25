using MyNotes.Contracts.V1;
using MyNotes.Services.InternalDto;
using System;
using System.Threading.Tasks;

namespace MyNotes.Services.ServiceContracts
{
    public interface IParagraphLogic
    {
        Task<BaseResponse> Create(ParagraphCreate paragraphCreate);
        Task<BaseResponse> Delete(Guid paragraphId, Guid userId);
        Task<BaseResponse> Get(ByEntityFilter entityByUserIdFilter);
        Task<BaseResponse> GetList(ByEntityFilter filter, PaginationFilter paginationFilter);
        Task<BaseResponse> Update(ParagraphUpdate entity);
    }
}