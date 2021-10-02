using MyNotes.Contracts.V1;
using System;
using System.Threading.Tasks;

namespace MyNotes.Services.ServiceContracts
{
    public interface IStartingPageLogic
    {
        Task<BaseResponse> Get(Guid userId);
    }
}
