using MyNotes.Domain.Entities.Additional;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Additional
{
    public interface ICommentContract
    {
        Task<Comment> Get(Guid id);

        Task<bool> Add(Comment entity);

        Task<bool> Remove(Guid ownerId, Guid id);

        Task<Comment> Update(Comment entity);

        Task<List<Comment>> GetListByParagraph(Guid ownerId, Guid paragraphId, int take, int skip);

        Task<bool> RemoveAllByParagraph(Guid ownerId, Guid paragraphId);

    }
}
