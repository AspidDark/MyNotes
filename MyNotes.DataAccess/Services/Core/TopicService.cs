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
    public class TopicService : BaseService<Topic>, ITopicContract
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<TopicService> _logger;

        public TopicService(AppDbContext appDbContext,
            ILogger<TopicService> logger) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<List<Topic>> GetList(Guid ownerId, int take, int skip)
        {
            var result = _appDbContext.Topics.Where(x => x.OwnerId == ownerId)
                .OrderBy(x=>x.EditDate)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()
                .ToList();
            return result;
        }

        public async Task<bool> RemoveAllByUser(Guid ownerId)
        {
            var topics = _appDbContext.Topics.Where(x => x.OwnerId == ownerId);
            if (topics is not null)
            {
                _appDbContext.Topics.RemoveRange(topics);
                var result = await _appDbContext.SaveChangesAsync();
                return result == topics.Count();
            }
            return true;
        }
    }
}
