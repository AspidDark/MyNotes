using MyNotes.Domain.Contracts.Core;
using MyNotes.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.DataAccess.Services
{
    public class TempService : ITopicService
    {
        private readonly AppDbContext _dbContext;

        public TempService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<bool> Add(Topic topic)
        {
            throw new NotImplementedException();
        }

        public async Task<Topic> Get(Guid id)
        {
            return new Topic();
        }

        public Task<List<Topic>> GetByUser(Guid ownerId, int take, int skip)
        {
            throw new NotImplementedException();
        }

        public Task<List<Topic>> GetList(Guid ownerId, int take, int skip)
        {
            throw new NotImplementedException();
        }

        public Task<Topic> Remove(Guid ownerId, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAllByUser(Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<Topic> Update(Topic comment)
        {
            throw new NotImplementedException();
        }
    }
}
