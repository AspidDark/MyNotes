using MyNotes.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Core
{
    public interface ITopicService
    {
        Task<Topic> Get(Guid id);

        Task<List<Topic>> GetList(Guid ownerId, int take, int skip);

        Task<bool> Add(Topic topic);

        Task<Topic> Remove(Guid ownerId, Guid id);

        Task<Topic> Update(Topic comment);

        Task<List<Topic>> GetByUser(Guid ownerId, int take, int skip);

        Task<bool> RemoveAllByUser(Guid ownerId);
    }
}
