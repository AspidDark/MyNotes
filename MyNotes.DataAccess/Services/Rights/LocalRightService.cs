using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyNotes.Domain.Contracts.Core;
using MyNotes.Domain.Contracts.Rights;
using MyNotes.Domain.Entities.Core;
using MyNotes.Domain.Entities.Rights;
using MyNotes.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.DataAccess.Services.Rights
{
    public class LocalRightService : BaseService<LocalRight>, ILocalRightContract
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<LocalRightService> _logger;

        public LocalRightService(AppDbContext appDbContext,
           ILogger<LocalRightService> logger) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<List<LocalRight>> GetUserAllowedResources(Guid ownerId, Guid userId, EntityType entityType, int take, int skip)
        {
            var result = _appDbContext.LocalRights.Where(x => x.OwnerId == ownerId 
            && x.AllowedUserId == userId
            && x.EntityType ==entityType)
                .OrderBy(x => x.EditDate)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()
                .ToList();
            return result;
        }

        public async Task<List<LocalRight>> GetUserRights(Guid ownerId, Guid userId, int take, int skip)
        {
            var result = _appDbContext.LocalRights.Where(x => x.OwnerId == ownerId
           && x.AllowedUserId == userId)
                .OrderBy(x => x.EditDate)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()
                .ToList();
            return result;
        }

        public async Task<LocalRight> GetUserRightToResource(Guid ownerId, Guid userId, Guid resourceId)
            => _appDbContext.LocalRights.AsNoTracking().FirstOrDefault(x => x.OwnerId == ownerId && x.AllowedUserId == userId && x.EntityId == resourceId);

        public async Task<bool> RemoveAllByUser(Guid ownerId)
        {
            var localRights = _appDbContext.LocalRights.Where(x => x.OwnerId == ownerId);
            if (localRights is not null)
            {
                _appDbContext.LocalRights.RemoveRange(localRights);
                var result = await _appDbContext.SaveChangesAsync();
                return result == localRights.Count();
            }
            return true;
        }
    }
}
