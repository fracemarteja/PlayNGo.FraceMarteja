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
    public class RoundService : BaseService<Round>, IRoundService
    {
        public RoundService(PlayNGoDBEntities db) : base(db) {
        }

        public async Task<object> GetHistoryByHandId(int handId)
        {
            return await GetAll().Where(t => t.Groups.Any(h => h.Hand_Id == handId))
                .Select(t => new
                {
                    t.Id,
                    Groups = t.Groups.Select(g => new
                        {
                            g.Id,
                            PlayerNames = g.PlayerGroups.Select(pg => pg.Player.Name)
                    }
                    ).ToList()
                }).ToListAsync();
        }
    }
}
