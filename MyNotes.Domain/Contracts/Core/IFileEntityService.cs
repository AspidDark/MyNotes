using MyNotes.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Core
{
    public interface IFileEntityService
    {
        Task<FileEntity> Get(Guid id);

        Task<bool> Add(FileEntity entity);

        Task<bool> Remove(Guid ownerId, Guid id);

        Task<FileEntity> Update(FileEntity entity);

        Task<List<FileEntity>> GetListByParagraph(Guid ownerId, Guid paragraphId, int take, int skip);

        Task<bool> RemoveAllByParagraph(Guid ownerId, Guid paragraphId);
    }
}
