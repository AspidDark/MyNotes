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
    public class GlobalRightService : BaseService<GlobalRight>, IGlobalRightContract
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<GlobalRight> _logger;

        public GlobalRightService(AppDbContext appDbContext,
           ILogger<GlobalRight> logger) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<GlobalRight> GetByOwnerAndRequesterId(Guid ownerId, Guid userId)
            => _appDbContext.GlobalRights.AsNoTracking().FirstOrDefault(x => x.OwnerId == ownerId && x.UserId == userId);

        public async Task<List<GlobalRight>> GetList(Guid ownerId, int take, int skip)
            => _appDbContext.GlobalRights
            .Where(x=>x.OwnerId==ownerId)
            .OrderBy(x => x.EditDate)
            .Skip(skip)
            .Take(take)
            .AsNoTracking()
            .ToList();

        public async Task<bool> RemoveAllByUser(Guid ownerId)
        {
            var globalRights = _appDbContext.GlobalRights.Where(x => x.OwnerId == ownerId);
            if (globalRights is not null)
            {
                _appDbContext.GlobalRights.RemoveRange(globalRights);
                var result = await _appDbContext.SaveChangesAsync();
                return result == globalRights.Count();
            }
            return true;
        }
    }
}
