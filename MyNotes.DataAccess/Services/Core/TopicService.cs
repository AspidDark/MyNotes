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
    public class TopicService : ITopicService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<TopicService> _logger;

        public TopicService(AppDbContext appDbContext,
            ILogger<TopicService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<Topic> Get(Guid id)
            => _appDbContext.Topics.AsNoTracking().FirstOrDefault(x => x.Id == id);

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

        public async Task<bool> Add(Topic topic)
        {
            topic.Id = Guid.NewGuid();
            topic.CreateDate = DateTime.Now;
            topic.EditDate = DateTime.Now;
             await _appDbContext.Topics.AddAsync(topic);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

        public virtual async Task<bool> Remove(Guid ownerId, Guid id)
        {
            var topic = _appDbContext.Topics.FirstOrDefault(x => x.OwnerId == ownerId && x.Id == id);
            if (topic is not null)
            {
                _appDbContext.Topics.Remove(topic);

                var result = await _appDbContext.SaveChangesAsync();
                return result > 0;
            }
            return true;
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

        public async Task<Topic> Update(Topic topic)
        {
            var result= _appDbContext.Topics.Update(topic);
            var updateResult = await _appDbContext.SaveChangesAsync();
            if (updateResult == 1)
            { 
                return result.Entity;
            }
            return null;
        }
    }
}
