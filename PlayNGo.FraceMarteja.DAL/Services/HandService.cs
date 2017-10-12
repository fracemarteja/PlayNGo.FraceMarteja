using PlayNGo.FraceMarteja.DAL.Contracts;
using PlayNGo.FraceMarteja.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayNGo.FraceMarteja.DAL.Services
{
    public class HandService: BaseService<Hand>, IHandService
    {
        public HandService(PlayNGoDBEntities db) : base(db) {
        }

        public async Task<List<Hand>> GetAllHandsOrderByRank()
        {
            return await GetAll().OrderBy(t => t.RankOrder).ToListAsync();
        }

        public async Task<object> GetAllHandsOrderByRankWithCount()
        {
            return await GetAll().OrderBy(t => t.RankOrder).Select(t => new { t.Id, t.Name, Count = t.Groups.Select(r => r.Round_Id).Distinct().Count() }).ToListAsync();
        }
    }
}
