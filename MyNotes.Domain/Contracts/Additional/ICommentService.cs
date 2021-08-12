using MyNotes.Domain.Entities.Additional;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Additional
{
    public interface ICommentService
    {
        Task<Comment> Get(Guid id);

        Task<List<Comment>> GetList(Guid ownerId, int take, int skip);

        Task<bool> Add(Comment comment);

        Task<Comment> Remove(Guid ownerId, Guid id);

        Task<Comment> Update(Comment comment);

        Task<List<Comment>> GetByParagraph(Guid ownerId, Guid ParagraphId, int take, int skip);

        Task<bool> RemoveAllByParagraph(Guid ownerId, Guid ParagraphId);

    }
}
