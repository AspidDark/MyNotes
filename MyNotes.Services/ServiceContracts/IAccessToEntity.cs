using MyNotes.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace MyNotes.Services.ServiceContracts
{
    public interface IAccessToEntity
    {
        Task<AccessType> CheckAccessToEntity(Guid ownerId, Guid userId, Guid resourceId);
    }
}
