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
    public class ParagraphService : BaseService<Paragraph>, IParagraphService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<ParagraphService> _logger;

        public ParagraphService(AppDbContext appDbContext,
           ILogger<ParagraphService> logger) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<List<Paragraph>> GetListByTopic(Guid ownerId, Guid topicId, int take, int skip)
        {
            var result = _appDbContext.Paragraphs.Where(x => x.OwnerId == ownerId && x.TopicId==topicId)
                .OrderBy(x => x.EditDate)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()
                .ToList();
            return result;
        }

        public async Task<bool> RemoveAllByTopic(Guid ownerId, Guid topicId)
        {
            var paragraphs = _appDbContext.Paragraphs
                .Where(x => x.OwnerId == ownerId && x.TopicId == topicId);

            if (paragraphs is not null)
            {
                _appDbContext.Paragraphs.RemoveRange(paragraphs);
                var result = await _appDbContext.SaveChangesAsync();
                return result == paragraphs.Count();
            }
            return true;
        }
    }
}
