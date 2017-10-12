using PlayNGo.FraceMarteja.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayNGo.FraceMarteja.DAL.Contracts
{
    public interface IHandService : IBaseService<Hand>
    {
        Task<List<Hand>> GetAllHandsOrderByRank();
        Task<object> GetAllHandsOrderByRankWithCount();
    }
}
