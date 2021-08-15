using MyNotes.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Core
{
    public interface INoteContract
    {
        Task<Note> Get(Guid id);

        Task<bool> Add(Note entity);

        Task<bool> Remove(Guid ownerId, Guid id);

        Task<Note> Update(Note entity);

        Task<List<Note>> GetListByParagraph(Guid ownerId, Guid paragraphId, int take, int skip);

        Task<bool> RemoveAllByParagraph(Guid ownerId, Guid paragraphId);
    }
}
