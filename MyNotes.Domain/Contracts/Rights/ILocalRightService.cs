using MyNotes.Domain.Entities.Rights;
using MyNotes.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Rights
{
    public interface ILocalRightService
    {
        Task<LocalRight> GetById(Guid rightId);

        Task<LocalRight> GetUserRightToResource(Guid ownerId, Guid userId, Guid resourceId);

        Task<List<LocalRight>> GetUserRights(Guid ownerId, Guid userId, int take, int skip);

        Task<List<LocalRight>> GetUserAllowedResources(Guid ownerId, Guid userId, EntityType entityType, int take, int skip);

        Task<LocalRight> AddRight(Guid ownerId, Guid userId, EntityType entityType, Guid entityId);

        Task<bool> Add(LocalRight right);

        Task<LocalRight> Remove(Guid id);

        Task<LocalRight> Update(LocalRight right);

        Task<bool> RemoveAllByUser(Guid userID);
    }
}
