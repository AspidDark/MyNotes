using MyNotes.Domain.Dto;
using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
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


        public virtual async Task<EntityAdd> Add(T t)
        {
            try
            {
                t.Id = Guid.NewGuid();
                t.CreateDate = DateTime.Now;
                t.EditDate = DateTime.Now;
                await _appDbContext.Set<T>().AddAsync(t);
                var result = await _appDbContext.SaveChangesAsync();
                return new EntityAdd { Result = result > 0, Id = t.Id };
            }
            catch (Exception ex)
            { 
                return new EntityAdd {Result=false };
            }
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
            t.EditDate = DateTime.Now;
            var result = _appDbContext.Set<T>().Update(t);
            var updateResult = await _appDbContext.SaveChangesAsync();
            if (updateResult == 1)
            {
                return result.Entity;
            }
            return null;
        }

        public async Task<List<T>> GetAllowedList(List<Guid> entityIds)
            => _appDbContext.Set<T>().Where(x => entityIds.Contains(x.Id)).ToList();
    }
}
