using MyNotes.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Core
{
    public interface IParagraphService
    {
        Task<Paragraph> Get(Guid id);

        Task<List<Paragraph>> GetList(Guid ownerId, int take, int skip);

        Task<bool> Add(Paragraph paragraph);

        Task<Paragraph> Remove(Guid ownerId, Guid id);

        Task<Paragraph> Update(Paragraph comment);

        Task<List<Paragraph>> GetByTopic(Guid ownerId, Guid TopicId, int take, int skip);

        Task<bool> RemoveAllByTopic(Guid ownerId, Guid TopicId);

    }
}
