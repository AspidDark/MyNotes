using MyNotes.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Core
{
    public interface INoteService
    {
        Task<Note> Get(Guid id);

        Task<List<Note>> GetList(Guid ownerId, int take, int skip);

        Task<bool> Add(Note note);

        Task<Note> Remove(Guid ownerId, Guid id);

        Task<Note> Update(Note note);

        Task<List<Note>> GetByParagraph(Guid ownerId, Guid ParagraphId, int take, int skip);

        Task<bool> RemoveAllByParagraph(Guid ownerId, Guid ParagraphId);
    }
}
