using MyNotes.Domain.Dto;
using MyNotes.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Core
{
    public interface IParagraphContract
    {
        Task<Paragraph> Get(Guid id);

        Task<EntityAdd> Add(Paragraph entity);

        Task<bool> Remove(Guid ownerId, Guid id);

        Task<Paragraph> Update(Paragraph entity);

        Task<List<Paragraph>> GetListByTopic(Guid ownerId, Guid topicId, int take, int skip);

        Task<bool> RemoveAllByTopic(Guid ownerId, Guid topicId);

    }
}
