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
    public class NoteService : BaseService<Note>, INoteContract
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<NoteService> _logger;

        public NoteService(AppDbContext appDbContext,
           ILogger<NoteService> logger) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<List<Note>> GetListByParagraph(Guid ownerId, Guid paragraphId, int take, int skip)
        {
            var result = _appDbContext.Notes.Where(x => x.OwnerId == ownerId && x.ParagraphId == paragraphId)
                .OrderBy(x => x.EditDate)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()
                .ToList();
            return result;
        }

        public async Task<bool> RemoveAllByParagraph(Guid ownerId, Guid paragraphId)
        {
            var notes = _appDbContext.Notes
               .Where(x => x.OwnerId == ownerId && x.ParagraphId == paragraphId);

            if (notes is not null)
            {
                _appDbContext.Notes.RemoveRange(notes);
                var result = await _appDbContext.SaveChangesAsync();
                return result == notes.Count();
            }
            return true;
        }

    }
}
