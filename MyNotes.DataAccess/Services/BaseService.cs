using MyNotes.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.DataAccess.Services
{
    public class BaseService<T> where T : BaseNoteEntity
    {
        private readonly AppDbContext _appDbContext;

        public BaseService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public virtual async Task<T> Get(Guid id)=>
            _appDbContext.Set<T>().FirstOrDefault(x => x.Id == id);


        public virtual async Task<bool> Add(T t)
        {
            t.Id = Guid.NewGuid();
            t.CreateDate = DateTime.Now;
            t.EditDate = DateTime.Now;
            await _appDbContext.Set<T>().AddAsync(t);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

        public virtual async Task<bool> Remove(Guid ownerId, Guid id)
        {
            var t = _appDbContext.Set<T>().FirstOrDefault(x => x.OwnerId == ownerId && x.Id == id);
            if (t is not null)
            {
                _appDbContext.Set<T>().Remove(t);
                var result = await _appDbContext.SaveChangesAsync();
                return result > 0;
            }
            return true;
        }

        public async Task<T> Update(T t)
        {
            var result = _appDbContext.Set<T>().Update(t);
            var updateResult = await _appDbContext.SaveChangesAsync();
            if (updateResult == 1)
            {
                return result.Entity;
            }
            return null;
        }
    }
}
