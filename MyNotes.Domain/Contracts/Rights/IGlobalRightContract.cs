using MyNotes.Domain.Entities.Rights;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Rights
{
    public interface IGlobalRightContract
    {
        Task<GlobalRight> Get(Guid rightId);
        Task<GlobalRight> Update(GlobalRight entity);
        Task<bool> Add(GlobalRight entity);
        Task<bool> Remove(Guid ownerId, Guid id);

        Task<GlobalRight> GetByOwnerAndRequesterId(Guid ownerId, Guid userId);

        Task<List<GlobalRight>> GetList(Guid ownerId, int take, int skip);

        Task<bool> RemoveAllByUser(Guid ownerId);
    }
}
