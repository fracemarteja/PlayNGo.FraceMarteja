using PlayNGo.FraceMarteja.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlayNGo.FraceMarteja.DAL
{
    public abstract class BaseService<T> : IBaseService<T>
        where T: class, new()
    {
        private PlayNGoDBEntities _db;
        private DbSet<T> _table = null;
        public BaseService(PlayNGoDBEntities db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        public async Task<T> GetOne(int id)
        {
            return await _table.Where("Id", id).FirstOrDefaultAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _table;
        }
        public async Task<DbResult> Delete(int id)
        {
            T entity = await GetOne(id);
            _table.Remove(entity);

            return await Save();

        }

        public async Task<DbResult> Edit(T t)
        {
            _table.Attach(t);
            _db.Entry(t).State = EntityState.Modified;

            return await Save();
        }               

        public async Task<DbResult> Insert(T t)
        {
            _table.Add(t);

            return await Save();
        }

        public async Task<DbResult> InsertRange(List<T> list)
        {
            _table.AddRange(list);

            return await Save();
        }

        private async Task<DbResult> Save()
        {
            DbResult returnValue = new DbResult() { IsSuccess = false };
            try
            {
                await _db.SaveChangesAsync();
                returnValue.IsSuccess = true;
            }
            catch (Exception ex)
            {
                returnValue.ErrorMessage = ex.Message;
            }

            return returnValue;
        }

        public void Dispose()
        {    
            if(_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }
    }
}
