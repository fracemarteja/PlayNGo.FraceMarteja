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
    public class PlayerService : BaseService<Player>, IPlayerService
    {
        public PlayerService(PlayNGoDBEntities db) : base(db) {
        }

        public async Task<List<Player>> GetPlayersOrderedByName()
        {
            return await GetAll().OrderBy(t => t.Name).ToListAsync();
        }

        public async Task<bool> IsAlreadyExists(string name)
        {
            return await GetAll().AnyAsync(t => t.Name == name);
        }
    }
}
