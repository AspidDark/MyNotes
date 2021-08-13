using MyNotes.Domain.Entities.Rights;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Rights
{
    public interface IGlobalRightService
    {
        Task<GlobalRight> GetById(Guid rightId);

        Task<GlobalRight> GetByOwnerAndRequesterId(Guid ownerId, Guid userId);

        Task<List<GlobalRight>> GetList(Guid ownerId, int take, int skip);

        Task<bool> Add(GlobalRight entity);

        Task<GlobalRight> Remove(Guid id);

        Task<GlobalRight> Update(GlobalRight entity);

        Task<bool> RemoveAllByUser(Guid ownerId);
    }
}
