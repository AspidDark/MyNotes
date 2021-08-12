using MyNotes.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Domain.Contracts.Core
{
    public interface IFileEntityService
    {
        Task<FileEntity> Get(Guid id);

        Task<List<FileEntity>> GetList(Guid ownerId, int take, int skip);

        Task<bool> Add(FileEntity fileEntity);

        Task<FileEntity> Remove(Guid ownerId, Guid id);

        Task<FileEntity> Update(FileEntity fileEntity);

        Task<List<FileEntity>> GetByNote(Guid ownerId, Guid NotehId, int take, int skip);

        Task<bool> RemoveAllByNote(Guid ownerId, Guid NoteId);
    }
}
