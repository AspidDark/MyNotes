using MyNotes.Contracts.V1;
using MyNotes.Services.InternalDto;
using System;
using System.Threading.Tasks;

namespace MyNotes.Services.ServiceContracts
{
    public interface IFileLogic
    {
        Task<BaseResponse> Create(FileCreate request);
        Task<BaseResponse> Delete(Guid fileId, Guid userId);
        Task<BaseResponse> Get(ByMainEntityFilter filter);
        Task<BaseResponse> CreateOrUpdateMessage(FileMessageUpdate request);
        Task<BaseResponse> GetMesage(ByMainEntityFilter filter);
        Task<BaseResponse> DeleteMessage(Guid fileId, Guid userId);
    }
}