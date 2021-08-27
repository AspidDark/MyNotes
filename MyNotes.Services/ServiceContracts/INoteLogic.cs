using MyNotes.Contracts.V1;
using MyNotes.Services.InternalDto;
using System;
using System.Threading.Tasks;

namespace MyNotes.Services.ServiceContracts
{
    public interface INoteLogic
    {
        Task<BaseResponse> Create(NoteCreate noteCreate);
        Task<BaseResponse> Delete(Guid noteId, Guid userId);
        Task<BaseResponse> Get(ByMainEntityFilter filter);
        Task<BaseResponse> GetList(ByEntityFilter filter, PaginationFilter paginationFilter);
        Task<BaseResponse> Update(NoteUpdate entity);
    }
}