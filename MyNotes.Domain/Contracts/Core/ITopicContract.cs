using MyNotes.Domain.Dto;
using MyNotes.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Core
{
    public interface ITopicContract
    {
        Task<Topic> Get(Guid id);

        Task<List<Topic>> GetList(Guid ownerId, int take, int skip);

        Task<EntityAdd> Add(Topic entity);

        Task<bool> Remove(Guid ownerId, Guid id);

        Task<Topic> Update(Topic entity);

        Task<bool> RemoveAllByUser(Guid ownerId);

        Task<List<Topic>> GetAllowedList(List<Guid> entityIds);
    }
}
