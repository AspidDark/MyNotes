using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyNotes.Domain.Contracts.Core;
using MyNotes.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.DataAccess.Services.Core
{
    public class FileEntityService : BaseService<FileEntity>, IFileEntityContract
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<FileEntityService> _logger;

        public FileEntityService(AppDbContext appDbContext,
           ILogger<FileEntityService> logger) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<List<FileEntity>> GetListByParagraph(Guid ownerId, Guid paragraphId, int take, int skip)
        {
            var result = _appDbContext.FileEntities.Where(x => x.OwnerId == ownerId && x.ParagraphId == paragraphId)
                .OrderBy(x => x.EditDate)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()
                .ToList();
            return result;
        }

        public async Task<bool> RemoveAllByParagraph(Guid ownerId, Guid paragraphId)
        {
            var files = _appDbContext.FileEntities
               .Where(x => x.OwnerId == ownerId && x.ParagraphId == paragraphId);

            if (files is not null)
            {
                _appDbContext.FileEntities.RemoveRange(files);
                var result = await _appDbContext.SaveChangesAsync();
                return result == files.Count();
            }
            return true;
        }
    }
}
