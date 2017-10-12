using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlayNGo.FraceMarteja.DAL
{
    public interface IBaseService<T>: IDisposable
    {
        Task<T> GetOne(int id);
        IQueryable<T> GetAll();
        Task<DbResult> Insert(T t);
        Task<DbResult> InsertRange(List<T> list);
        Task<DbResult> Edit(T t);
        Task<DbResult> Delete(int id);
    }
}
