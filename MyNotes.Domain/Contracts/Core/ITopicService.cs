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

        Task<bool> Remove(Guid ownerId, Guid id);

        Task<Topic> Update(Topic topic);

        Task<bool> RemoveAllByUser(Guid ownerId);
    }
}
