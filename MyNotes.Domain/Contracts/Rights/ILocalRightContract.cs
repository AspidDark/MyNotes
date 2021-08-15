using MyNotes.Domain.Entities.Rights;
using MyNotes.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Rights
{
    public interface ILocalRightContract
    {
        Task<LocalRight> Get(Guid rightId);
        Task<bool> Add(LocalRight entity);

        Task<bool> Remove(Guid ownerId, Guid id);

        Task<LocalRight> Update(LocalRight entity);

        Task<LocalRight> GetUserRightToResource(Guid ownerId, Guid userId, Guid resourceId);

        Task<List<LocalRight>> GetUserRights(Guid ownerId, Guid userId, int take, int skip);

        Task<List<LocalRight>> GetUserAllowedResources(Guid ownerId, Guid userId, EntityType entityType, int take, int skip);

        Task<bool> RemoveAllByUser(Guid ownerId);
    }
}
