using MyNotes.Domain.Dto;
using MyNotes.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Core
{
    public interface IFileEntityContract
    {
        Task<FileEntity> Get(Guid id);

        Task<EntityAdd> Add(FileEntity entity);

        Task<bool> Remove(Guid ownerId, Guid id);

        Task<FileEntity> Update(FileEntity entity);

        Task<List<FileEntity>> GetListByParagraph(Guid ownerId, Guid paragraphId, int take, int skip);

        Task<bool> RemoveAllByParagraph(Guid ownerId, Guid paragraphId);
    }
}
