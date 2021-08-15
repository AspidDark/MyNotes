using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNotes.Domain.Entities.Additional;
using MyNotes.Domain.Contracts.Additional;

namespace MyNotes.DataAccess.Services.Additional
{
    public class CommentService : BaseService<Comment>, ICommentContract
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CommentService> _logger;

        public CommentService(AppDbContext appDbContext,
           ILogger<CommentService> logger) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<List<Comment>> GetListByParagraph(Guid ownerId, Guid paragraphId, int take, int skip)
        {
            var result = _appDbContext.Comments.Where(x => x.OwnerId == ownerId && x.ParagraphId == paragraphId)
               .OrderBy(x => x.EditDate)
               .Skip(skip)
               .Take(take)
               .AsNoTracking()
               .ToList();
            return result;
        }

        public async Task<bool> RemoveAllByParagraph(Guid ownerId, Guid paragraphId)
        {
            var comments = _appDbContext.Comments
             .Where(x => x.OwnerId == ownerId && x.ParagraphId == paragraphId);

            if (comments is not null)
            {
                _appDbContext.Comments.RemoveRange(comments);
                var result = await _appDbContext.SaveChangesAsync();
                return result == comments.Count();
            }
            return true;
        }

    }
}
